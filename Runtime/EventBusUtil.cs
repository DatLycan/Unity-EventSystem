using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DatLycan.Packages.EventSystem {
    public static class EventBusUtil {
        private static IReadOnlyList<Type> EventTypes { get; set; }
        private static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state) {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingEditMode) ClearAllBuses();
        }
#endif
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        private static List<Type> InitializeAllBuses() {
            List<Type> eventBusTypes = new();

            Type typeDefinition = typeof(EventBus<>);
            foreach (Type eventType in EventTypes) {
                Type busType = typeDefinition.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
                Debug.Log($"Initialized EventBus<{eventType.Name}>");
            }

            return eventBusTypes;
        }

        private static void ClearAllBuses() {
            Debug.Log("Clearing all buses...");
            for (int i = 0; i < EventBusTypes.Count; i++) {
                Type busType = EventBusTypes[i];
                MethodInfo clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod!.Invoke(null, null);
            }
        }
    }
}