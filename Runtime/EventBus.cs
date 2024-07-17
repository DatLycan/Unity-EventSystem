using System.Collections.Generic;
using UnityEngine;

namespace DatLycan.Packages.EventSystem {
    public static class EventBus<T> where T : IEvent {
        private static readonly HashSet<IEventBinding<T>> bindings = new();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Unregister(EventBinding<T> binding) => bindings.Remove(binding);

        // ReSharper disable Unity.PerformanceAnalysis
        public static void Raise(T @event) {
            foreach (IEventBinding<T> binding in bindings) {
                if (@event.ShouldLog()) Debug.Log($"Raising event: {typeof(T).Name}");
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
        
        private static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}
