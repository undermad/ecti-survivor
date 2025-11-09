using Explorer._Project.Scripts.EventBus.Events;
using Explorer._Scripts.Explorer.Systems.CombatSystem;

namespace Explorer._Scripts.Explorer.Components.Abilities
{
    public struct ProjectileAbilityActivatedEvent : IEvent
    {
        public Projectile ProjectilePrefab;
        public AbilitySystemComponent ownerAsc;
    }
}