using System.Collections.Generic;
using UnityEngine;
using Timer = Explorer._Project.Scripts.Utils.Timer.Timer;

namespace Explorer._Project.Scripts.World
{
    public static class CooldownManager
    {
        // need to use different structure to quickly add and remove
        private static readonly HashSet<Timer> timers = new();

        public static void AddTimers(params Timer[] newTimers)
        {
            foreach (var newTimer in newTimers)
            {
                timers.Add(newTimer);
            }
        }
        
        public static void RemoveTimer(params Timer[] timersToRemove)
        {
            foreach (var timer in timersToRemove)
            {
                timers.Remove(timer);
            }
        }

        public static void TickAllTimers()
        {
            var deltaTime = Time.deltaTime;
            foreach (var timer in timers)
            {
                timer.Tick(deltaTime);
            }
        }
    }
}