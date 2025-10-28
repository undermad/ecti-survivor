using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.LifecycleManager
{
    public class FixedUpdateManager : MonoBehaviour
    {
        private static readonly List<IFixedUpdateObserver> Observers = new();
        private static readonly List<IFixedUpdateObserver> PendingObservers = new();
        private static int _index;
        private void FixedUpdate()
        {
            for (_index = Observers.Count - 1; _index >= 0; _index--)
            {
                Observers[_index].CustomFixedUpdate();
            }
            Observers.AddRange(PendingObservers);
            PendingObservers.Clear();
        }

        public static void Register(IFixedUpdateObserver observer)
        {
            PendingObservers.Add(observer);
        }

        public static void Unregister(IFixedUpdateObserver observer)
        {
            Observers.Remove(observer);
            
        }
    }
}