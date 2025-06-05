using Explorer._Project.Scripts.EventBus.Events;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Events
{
    public struct MoveEvent : IEvent
    {
        public Vector2 Direction;
    }
}