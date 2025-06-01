using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Project.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.EventBus
{
    public static class EventBusUtils
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set;}

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
        
        public static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;
            if (PlayModeState == PlayModeStateChange.ExitingEditMode)
            {
                ClearAllBuses();
            }
        }
#endif
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialise()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        static List<Type> InitializeAllBuses()
        {
            var typedef = typeof(EventBus<>);
            return EventTypes.Select(eventType => typedef.MakeGenericType(eventType)).ToList();
        }

        public static void ClearAllBuses()
        {
            Debug.Log("Clearing all buses");
            foreach (var busType in EventBusTypes)
            {
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                if (clearMethod != null)
                {
                    clearMethod.Invoke(null, null);
                }
            }
        }
    }
}