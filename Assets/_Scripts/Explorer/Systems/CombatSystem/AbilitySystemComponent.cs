using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        public List<AttributeSet> Attributes = new();
        public List<GameplayAbility> GrantedAbilities = new();
        public GameplayTagContainer Tags = new();
        
        // Runtime state
        private readonly Dictionary<string, Attribute> _attributes = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<ActiveEffect> _activeEffects = new();

        private void Awake()
        {
            foreach (var set in Attributes)
            {
                var dictionary = set.InstantiateDict();
                foreach (var pair in dictionary)
                    _attributes[pair.Key] = pair.Value;
            }

            foreach (var attribute in _attributes.Values)
            {
                attribute.Clamp();
            }
        }


        private void Update()
        {
            TickEffects(Time.deltaTime);
        }


// ---- Public API ----
        public Attribute GetAttribute(string name) => _attributes.TryGetValue(name, out var a) ? a : null;
        public float GetAttributeValue(string name) => GetAttribute(name)?.CurrentValue ?? 0f;
        public void SetAttributeBase(string name, float baseValue)
        {
            if (_attributes.TryGetValue(name, out var a)) { a.BaseValue = baseValue; a.Clamp(); }
        }


        public GameplayEffectSpec MakeSpec(GameplayEffectDef def, float level, GameObject instigator)
        {
            var spec = new GameplayEffectSpec { Def = def, Level = level, Instigator = instigator };
            if (def != null && def.Modifiers != null)
            {
                foreach (var mod in def.Modifiers)
                    spec.ResolvedMagnitudes[mod.AttributeName] = mod.Magnitude.Evaluate(level);
            }
            return spec;
        }
        
        public bool ApplyEffectSpec(GameplayEffectSpec spec, GameObject targetObj = null)
        {
            var target = targetObj ? targetObj.GetComponent<AbilitySystemComponent>() : this;
            if (target == null) target = this;


// Check tag requirements on target
            foreach (var t in spec.Def.RequiredTargetTags?.Tags ?? Array.Empty<GameplayTag>())
                if (!target.Tags.HasTag(t)) return false;
            foreach (var t in spec.Def.BlockedTargetTags?.Tags ?? Array.Empty<GameplayTag>())
                if (target.Tags.HasTag(t)) return false;


// Grant tags immediately
            foreach (var t in spec.Def.GrantedTags?.Tags ?? Array.Empty<GameplayTag>())
                target.Tags.AddTag(t);


            if (spec.Def.Policy == DurationPolicy.Instant)
            {
                ApplyModifiers(target, spec);
                return true;
            }


// Check if there is an existing stackable effect of same def
            var existing = _activeEffects.Find(ae => ae.Spec.Def == spec.Def && ae.Spec.Instigator == spec.Instigator && ae.Spec.Level == spec.Level);
            if (existing != null && spec.Def.CanStack)
            {
                existing.stacks = Mathf.Min(existing.stacks + 1, Mathf.Max(1, spec.Def.MaxStacks));
                if (spec.Def.RefreshDurationOnStack)
                    existing.timeRemaining = spec.Def.GetDuration(spec.Level);
                return true;
            }


            var active = new ActiveEffect(spec);
            _activeEffects.Add(active);
// Apply on‑application modifiers if desired (common GAS pattern: apply once and/or periodic)
            ApplyModifiers(target, spec);
            return true;
        }
        
        private void ApplyModifiers(AbilitySystemComponent target, GameplayEffectSpec spec)
        {
            foreach (var mod in spec.Def.Modifiers)
            {
                var magnitude = spec.ResolvedMagnitudes[mod.AttributeName];
                var attr = target.GetAttribute(mod.AttributeName);
                if (attr == null) continue; // optional: create on the fly


                switch (mod.Operation)
                {
                    case ModifierOp.Add:
                        attr.CurrentValue += magnitude; break;
                    case ModifierOp.Multiply:
                        attr.CurrentValue *= magnitude; break;
                    case ModifierOp.Override:
                        attr.CurrentValue = magnitude; break;
                    case ModifierOp.ClampMin:
                        attr.Min = Mathf.Max(attr.Min, magnitude); break;
                    case ModifierOp.ClampMax:
                        attr.Max = Mathf.Min(attr.Max, magnitude); break;
                }
                attr.Clamp();
            }
        }
        
        private void TickEffects(float deltaTime)
        {
            for (var index = _activeEffects.Count - 1; index >= 0; index--)
            {
                var activeEffect = _activeEffects[index];
                if (activeEffect.Spec.Def.Policy == DurationPolicy.Duration)
                {
                    activeEffect.timeRemaining -= deltaTime;
                }


                if (activeEffect.Spec.Def.IsPeriodic)
                {
                    activeEffect.periodTimer -= deltaTime;
                    if (activeEffect.periodTimer <= 0f)
                    {
                        ApplyModifiers(this, activeEffect.Spec);
                        activeEffect.periodTimer = activeEffect.Spec.Def.GetPeriod(activeEffect.Spec.Level);
                    }
                }


                if (activeEffect.IsExpired)
                {
                    foreach (var t in activeEffect.Spec.Def.GrantedTags?.Tags ?? Array.Empty<GameplayTag>())
                    {
                        Tags.RemoveTag(t);
                    }
                    _activeEffects.RemoveAt(index);
                }
            }
        }
        
        public bool TryActivateAbility(GameplayAbility ability, GameObject target = null)
        {
            if (ability == null) return false;
            if (!GrantedAbilities.Contains(ability)) return false;
            if (!ability.CanActivate(this)) return false;
            ability.Activate(this, target);
            return true;
        }
        
    }
}