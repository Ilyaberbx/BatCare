using System;
using System.Collections.Generic;
using UnityEngine;
using Workspace.Services.EventBus.Abstractions;

namespace Workspace.Services.EventBus
{
    internal class InternalEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<IEventListener>> _listenersByType = new();
        private readonly Dictionary<Type, List<IPriorityEventListener>> _priorityListenersByType = new();

        #region Subscribe and Unsubscribe
        
        public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent
        {
            var eventType = typeof(TEvent);

            if (_listenersByType.TryGetValue(eventType, out var listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                listeners = new List<IEventListener>();
                _listenersByType[eventType] = listeners;
            }
        }

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent
        {
            var eventType = typeof(TEvent);

            if (_listenersByType.TryGetValue(eventType, out var listeners))
            {
                listeners.Remove(listener);
            }
        }
        #endregion

        #region Events Invokation
        
        public void Raise<TEvent>(TEvent eventData) where TEvent : struct, IEvent
        {
            var eventType = typeof(TEvent);

            if (_listenersByType.TryGetValue(eventType, out var listeners))
            {
                foreach (var derivedListener in listeners)
                {
                    if (derivedListener is IEventListener<TEvent> listener)
                    {
                        listener.OnEvent(eventData);
                    }
                    else
                    {
                        Debug.LogException(new InvalidCastException("Can not cast derived listener to: " + typeof(IEventListener<TEvent>)));
                    }
                }
            }
            else
            {
                Debug.LogWarning("Can raise event of type: " + eventType);
            }
        }
        
        #endregion
    }
}