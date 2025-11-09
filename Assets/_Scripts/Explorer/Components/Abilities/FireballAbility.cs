using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem.Abilities
{
    [CreateAssetMenu(menuName = "GAS/Samples/FireballAbility", fileName = "GA_Fireball")]
    public class FireballAbility : GameplayAbility
    {
        public GameplayEffectDef directDamage;
        public GameplayEffectDef burnDot;

        public override bool CanActivate(AbilitySystemComponent abilitySystemComponent)
        {
            return false;
        }

        public override void Activate(AbilitySystemComponent abilitySystemComponent, GameObject target)
        {
            if (!CanActivate(abilitySystemComponent))
            {
                return;
            }

            // Cost & cooldown
            if (CostEffect)
                abilitySystemComponent.ApplyEffectSpec(abilitySystemComponent.MakeSpec(CostEffect, AbilityLevel,
                    abilitySystemComponent.gameObject));
            if (CooldownEffect)
                abilitySystemComponent.ApplyEffectSpec(abilitySystemComponent.MakeSpec(CooldownEffect, AbilityLevel,
                    abilitySystemComponent.gameObject));


            // Payload
            if (directDamage)
                abilitySystemComponent.ApplyEffectSpec(
                    abilitySystemComponent.MakeSpec(directDamage, AbilityLevel, abilitySystemComponent.gameObject),
                    target);
            if (burnDot)
                abilitySystemComponent.ApplyEffectSpec(
                    abilitySystemComponent.MakeSpec(burnDot, AbilityLevel, abilitySystemComponent.gameObject), target);

        }
    }
}