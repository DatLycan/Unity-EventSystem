﻿using System;

namespace DatLycan.Packages.EventSystem {
    internal interface IEventBinding<T>{
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
    
    public class EventBinding<T> : IEventBinding<T> where T : IEvent {
        private Action<T> OnEvent = _ => { };
        private Action OnEventNoArgs = () => { };
        
        Action<T> IEventBinding<T>.OnEvent {
            get => OnEvent;
            set => OnEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs {
            get => OnEventNoArgs;
            set => OnEventNoArgs = value;
        }

        public EventBinding(Action<T> onEvent) => this.OnEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => this.OnEventNoArgs = onEventNoArgs;

        public void Add(Action onEvent) => OnEventNoArgs += onEvent;
        public void Remove(Action onEvent) => OnEventNoArgs -= onEvent;
        
        public void Add(Action<T> onEvent) => this.OnEvent += onEvent;
        public void Remove(Action<T> onEvent) => this.OnEvent -= onEvent;
    }
}