using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.Abilities
{
    [CreateAssetMenu(menuName = "GAS/BaseRangeAttack", fileName = "GA_BaseRangeAttack_")]
    public class BaseRangeAttack : GameplayAbility
    {
        [SerializeField, Anywhere] private Projectile projectilePrefab;

        private Vector3 _direction;
        
        public override void Activate(AbilitySystemComponent ownerAsc, GameObject target)
        {
            base.Activate(ownerAsc, target);
            SpawnProjectile(ownerAsc);
        }

        private void SpawnProjectile(AbilitySystemComponent abilitySystemComponent)
        {
            var ownerId = abilitySystemComponent.GetOwnerId();
            EventBus<ProjectileAbilityActivatedEvent>.Publish(ownerId, new ProjectileAbilityActivatedEvent
            {
                ownerAsc = abilitySystemComponent,
                ProjectilePrefab = projectilePrefab
            });
        }
    }
}