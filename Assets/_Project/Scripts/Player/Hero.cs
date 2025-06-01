using _Project.Scripts.EventBus;
using _Project.Scripts.EventBus.Events;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Hero : MonoBehaviour
    {
        private EventBinding<TestEvent> _testEventBinding;

        private void OnEnable()
        {
            _testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
            EventBus<TestEvent>.Subscribe(_testEventBinding);
        }

        private void OnDisable()
        {
            EventBus<TestEvent>.UnSubscribe(_testEventBinding);
        }
        void HandleTestEvent(TestEvent @event)
        {
            Debug.Log(@event.Message);
        }
    }
}