using System;
using System.Collections.Generic;
using Explorer._Scripts.Explorer.Objects;
using Explorer._Scripts.Explorer.Systems.LifecycleManager;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    public class AbilitySystemComponent : MonoBehaviour, IUpdateObserver
    {
        [SerializeField, Parent] private PersistentId persistentId;
        public List<AttributeSet> Attributes = new();
        public List<GameplayAbility> GrantedAbilities = new();
        public GameplayTagContainer Tags = new();

        // Runtime state
        private readonly Dictionary<string, Attribute> attributes = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<ActiveEffect> activeEffects = new();
        
        public Guid GetOwnerId() => persistentId.ID;
        public bool IsEqual(AbilitySystemComponent other) => other.GetOwnerId().Equals(GetOwnerId());

        private void Awake()
        {
            
            foreach (var set in Attributes)
            {
                var dictionary = set.InstantiateDict();
                foreach (var pair in dictionary)
                {
                    attributes[pair.Key] = pair.Value;
                }
            }
        }

        private void OnEnable()
        {
            UpdateManager.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.Unregister(this);
        }

        private void Start()
        {
            foreach (var attribute in attributes.Values)
            {
                attribute.SetCurrentValue(attribute.CurrentValue, persistentId.ID);
            }
        }
        
        public void CustomUpdate()
        {
            TickEffects(Time.deltaTime);
        }

        public Attribute GetAttribute(string attributeName) => attributes.GetValueOrDefault(attributeName);
        public float GetAttributeValue(string attributeName) => GetAttribute(attributeName)?.CurrentValue ?? 0f;


        public GameplayEffectSpec MakeSpec(GameplayEffectDef definition, int level, GameObject instigator)
        {
            var specification = new GameplayEffectSpec
            {
                Def = definition,
                Level = level,
                Instigator = instigator
            };
            if (definition && definition.Modifiers != null)
            {
                foreach (var modifier in definition.Modifiers)
                {
                    specification.ResolvedMagnitudes[modifier.AttributeName.Value] = modifier.Magnitude.Evaluate(level);
                }
            }

            return specification;
        }

        public bool ApplyEffectSpec(GameplayEffectSpec specification, GameObject targetObject = null)
        {
            var target = targetObject ? targetObject.GetComponent<AbilitySystemComponent>() : this;
            if (!target)
            {
                target = this;
            }

            // Check tag requirements on target
            foreach (var requiredTag in specification.Def.RequiredTargetTags?.Tags ?? Array.Empty<GameplayTag>())
            {
                if (!target.Tags.HasTag(requiredTag))
                    return false;
            }

            foreach (var blockingTag in specification.Def.BlockedTargetTags?.Tags ?? Array.Empty<GameplayTag>())
            {
                if (target.Tags.HasTag(blockingTag))
                    return false;
            }

            // Grant tags immediately
            foreach (var grantedTag in specification.Def.GrantedTags?.Tags ?? Array.Empty<GameplayTag>())
            {
                target.Tags.AddTag(grantedTag, persistentId.ID);
            }

            if (specification.Def.Policy == DurationPolicy.Instant)
            {
                ApplyModifiers(target, specification);
                return true;
            }


            // Check if there is an existing stackable effect of same definition
            var existing = activeEffects.Find(activeEffect =>
                activeEffect.Spec.Def == specification.Def &&
                activeEffect.Spec.Instigator == specification.Instigator &&
                activeEffect.Spec.Level == specification.Level);

            if (existing != null && specification.Def.CanStack)
            {
                existing.stacks = Mathf.Min(existing.stacks + 1, Mathf.Max(1, specification.Def.MaxStacks));
                if (specification.Def.RefreshDurationOnStack)
                {
                    existing.timeRemaining = specification.Def.GetDuration(specification.Level);
                }

                return true;
            }

            var active = new ActiveEffect(specification);
            activeEffects.Add(active);
            // Apply on‑application modifiers if desired (common GAS pattern: apply once and/or periodic)
            ApplyModifiers(target, specification);
            return true;
        }

        private void ApplyModifiers(AbilitySystemComponent target, GameplayEffectSpec spec)
        {
            foreach (var mod in spec.Def.Modifiers)
            {
                var magnitude = spec.ResolvedMagnitudes[mod.AttributeName.Value];
                var attribute = target.GetAttribute(mod.AttributeName.Value);
                if (attribute == null) continue; // optional: create on the fly


                switch (mod.Operation)
                {
                    case ModifierOp.Add:
                        attribute.SetCurrentValue(attribute.CurrentValue + magnitude, persistentId.ID); break;
                    case ModifierOp.Multiply:
                        attribute.SetCurrentValue(attribute.CurrentValue * magnitude, persistentId.ID); break;
                    case ModifierOp.Override:
                        attribute.SetCurrentValue(magnitude, persistentId.ID); break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void TickEffects(float deltaTime)
        {
            for (var index = activeEffects.Count - 1; index >= 0; index--)
            {
                var activeEffect = activeEffects[index];
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
                    foreach (var grantedTag in activeEffect.Spec.Def.GrantedTags?.Tags ?? Array.Empty<GameplayTag>())
                    {
                        Tags.RemoveTag(grantedTag, persistentId.ID);
                    }

                    if (activeEffect.Spec.Def.Policy.Equals(DurationPolicy.Duration) && !activeEffect.Spec.Def.IsPeriodic)
                    {
                        RemoveModifiers(activeEffect);
                    }
                    activeEffects.RemoveAt(index);
                }
            }
        }

        private void RemoveModifiers(ActiveEffect activeEffect)
        {
            foreach (var modifier in activeEffect.Spec.Def.Modifiers)
            {
                var attribute = GetAttribute(modifier.AttributeName.Value);
                var magnitude = activeEffect.Spec.ResolvedMagnitudes[modifier.AttributeName.Value];
                if (attribute == null) continue;

                switch (modifier.Operation)
                {
                    case ModifierOp.Add:
                        attribute.SetCurrentValue(attribute.CurrentValue - magnitude, persistentId.ID);
                        break;
                    case ModifierOp.Multiply:
                        attribute.SetCurrentValue(attribute.CurrentValue / magnitude, persistentId.ID);
                        break;
                    case ModifierOp.Override:
                        attribute.SetCurrentValue(magnitude, persistentId.ID);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public bool TryActivateAbility(GameplayAbility ability, GameObject target = null)
        {
            if (!ability) return false;
            if (!GrantedAbilities.Contains(ability)) return false;
            if (!ability.CanActivate(this)) return false;
            ability.Activate(this, target);
            return true;
        }
    }
}