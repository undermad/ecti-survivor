using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.EventBus
{
    public static class EventBus<T> where T : IEvent 
    {
        private static readonly HashSet<IEventBinding<T>> Bindings = new();
        
        public static void Subscribe(IEventBinding<T> binding) => Bindings.Add(binding);
        public static void UnSubscribe(IEventBinding<T> binding) => Bindings.Remove(binding);

        public static void Publish(T @event)
        {
            foreach (var binding in Bindings)
            {
                binding.OnEvent?.Invoke(@event);
                binding.OnEventNoArgs?.Invoke();
            }
        }

        static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            Bindings.Clear();
        }
    }
}