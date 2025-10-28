using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.LifecycleManager
{
    public class LateUpdateManager : MonoBehaviour
    {
        private static readonly List<ILateUpdateObserver> Observers = new();
        private static readonly List<ILateUpdateObserver> PendingObservers = new();
        private static int _index;
        private void LateUpdate()
        {
            for (_index = Observers.Count - 1; _index >= 0; _index--)
            {
                Observers[_index].CustomLateUpdate();
            }
            Observers.AddRange(PendingObservers);
            PendingObservers.Clear();
        }

        public static void Register(ILateUpdateObserver observer)
        {
            PendingObservers.Add(observer);
        }

        public static void Unregister(ILateUpdateObserver observer)
        {
            Observers.Remove(observer);
            
        }
    }
}