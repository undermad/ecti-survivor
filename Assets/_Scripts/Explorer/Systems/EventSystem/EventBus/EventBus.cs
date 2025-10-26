using System;
using System.Collections.Generic;
using System.Linq;
using Explorer._Project.Scripts.EventBus.Events;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> Globals = new();
        private static readonly Dictionary<Guid, HashSet<IEventBinding<T>>> ByGuid = new();

        public static void Subscribe(IEventBinding<T> binding) => Globals.Add(binding);
        public static void UnSubscribe(IEventBinding<T> binding) => Globals.Remove(binding);

        public static void Publish(T @event)
        {
            foreach (var binding in Globals)
            {
                binding.OnEvent?.Invoke(@event);
                binding.OnEventNoArgs?.Invoke();
            }
        }
        
        public static void Publish(Guid key, T @event)
        {
            if (ByGuid.TryGetValue(key, out var set))
            {
                foreach (var binding in set.ToArray())
                {
                    binding.OnEvent?.Invoke(@event);
                    binding.OnEventNoArgs?.Invoke();
                }
            }

            Publish(@event);
        }

        public static void Subscribe(Guid key, IEventBinding<T> binding)
        {
            if (!ByGuid.TryGetValue(key, out var set))
            {
                ByGuid.Add(key, set = new HashSet<IEventBinding<T>>());
            }
            set.Add(binding);
        }

        public static void UnSubscribe(Guid key, IEventBinding<T> binding)
        {
            if (!ByGuid.TryGetValue(key, out var set))
            {
                return;
            }
            
            set.Remove(binding);
            if (set.Count == 0)
            {
                ByGuid.Remove(key);
            }
        }

        public static void Publish(ReadOnlySpan<Guid> keys, T @event, bool includeGlobals)
        {
            HashSet<IEventBinding<T>>? called = null;

            foreach (var key in keys)
            {
                if (!ByGuid.TryGetValue(key, out var set))
                {
                    continue;
                }

                foreach (var binding in set.ToArray())
                {
                    called ??= new HashSet<IEventBinding<T>>();
                    if (!called.Add(binding)) continue;

                    binding.OnEvent?.Invoke(@event);
                    binding.OnEventNoArgs?.Invoke();
                }
            }

            if (!includeGlobals)
            {
                return;
            }

            foreach (var binding in Globals.ToArray())
            {
                if (called != null && !called.Add(binding)) continue;
                binding.OnEvent?.Invoke(@event);
                binding.OnEventNoArgs?.Invoke();
            }
        }

        static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            Globals.Clear();
            ByGuid.Clear();
        }
    }
}