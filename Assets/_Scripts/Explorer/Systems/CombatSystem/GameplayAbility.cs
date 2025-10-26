using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "GAS/Ability", fileName = "Ability")]
    public class GameplayAbility : ScriptableObject
    {
        public string DisplayName;
        public string Description;
        public Sprite Icon;

        public GameplayTagContainer GrantedTags = new();
        public GameplayTagContainer ActivationRequiredTags = new();
        public GameplayTagContainer ActivationBlockedTags = new();

        public GameplayEffectDef CostEffect;
        public GameplayEffectDef CooldownEffect;

        public List<GameplayEffectDef> EffectsToApply = new();

        public int AbilityLevel = 1;

        public virtual bool CanActivate(AbilitySystemComponent abilitySystemComponent)
        {
            if (ActivationRequiredTags != null)
            {
                foreach (var tag in ActivationRequiredTags.Tags)
                {
                    if (!abilitySystemComponent.Tags.HasTag(tag))
                    {
                        return false;
                    }
                }
            }

            if (ActivationBlockedTags != null)
            {
                foreach (var tag in ActivationBlockedTags.Tags)
                {
                    if (!abilitySystemComponent.Tags.HasTag(tag))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public virtual void Activate(AbilitySystemComponent asc, GameObject target)
        {
            if (!CanActivate(asc)) return;
            if (CostEffect)
                asc.ApplyEffectSpec(asc.MakeSpec(CostEffect, AbilityLevel, asc.gameObject));

            if (CooldownEffect)
                asc.ApplyEffectSpec(asc.MakeSpec(CooldownEffect, AbilityLevel, asc.gameObject));

            foreach (var eff in EffectsToApply)
                asc.ApplyEffectSpec(asc.MakeSpec(eff, AbilityLevel, asc.gameObject), target);
        }

    }
}