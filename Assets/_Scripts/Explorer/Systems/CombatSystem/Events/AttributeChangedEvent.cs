using System;
using Explorer._Project.Scripts.EventBus.Events;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem.Events
{
    public struct AttributeChangedEvent : IEvent
    {
        public Guid Owner;
        public string AttributeName;
        public float NewValue;
        public float OldValue;
        public float Min;
        public float Max;
    }
}