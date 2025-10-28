using System;
using Explorer._Project.Scripts.EventBus.Events;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem.Events
{
    public class TagRemovedEvent : IEvent
    {
        public Guid Owner;
        public string TagName;
    }
}