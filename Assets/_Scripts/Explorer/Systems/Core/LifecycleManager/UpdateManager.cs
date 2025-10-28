using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.LifecycleManager
{
    public class UpdateManager : MonoBehaviour
    {
        private static readonly List<IUpdateObserver> Observers = new();
        private static readonly List<IUpdateObserver> PendingObservers = new();
        private static int _index;
        private void Update()
        {
            for (_index = Observers.Count - 1; _index >= 0; _index--)
            {
                Observers[_index].CustomUpdate();
            }
            Observers.AddRange(PendingObservers);
            PendingObservers.Clear();
        }

        public static void Register(IUpdateObserver observer)
        {
            PendingObservers.Add(observer);
        }

        public static void Unregister(IUpdateObserver observer)
        {
            Observers.Remove(observer);
            
        }
    }
}