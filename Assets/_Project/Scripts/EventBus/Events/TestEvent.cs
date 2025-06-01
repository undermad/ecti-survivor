using System;

namespace _Project.Scripts.EventBus.Events
{
    public struct TestEvent : IEvent
    {
        public string Message;
    }
}