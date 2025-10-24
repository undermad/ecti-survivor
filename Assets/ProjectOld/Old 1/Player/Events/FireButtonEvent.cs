using Explorer._Project.Scripts.EventBus.Events;
using UnityEngine.InputSystem;

namespace Explorer._Project.Scripts.Player.Events
{
    public struct FireButtonEvent : IEvent
    {
        public InputActionPhase Phase;
    }
}