using Explorer._Project.Scripts.EventBus.Events;

namespace Explorer._Project.Scripts.Player.Events
{
    public struct StateEndEvent : IEvent
    {
        public string StateName;
    }
}