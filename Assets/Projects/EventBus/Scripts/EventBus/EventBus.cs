using System;
using System.Collections.Generic;

namespace EventBus
{
    public sealed class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> eventHandlers = new();

        public void SubscribeHandler<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);
            
            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new EventHandlerCollection<TEvent>();
            }
            
            eventHandlers[eventType].Subscribe(handler);
        }

        public void UnsubscribeHandler<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);
            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Unsubscribe(handler);
            }
        }
        
        public void RaiseEvent<TEvent>(TEvent evt)
        {
            Type eventType = evt.GetType();
            if (eventHandlers.TryGetValue(eventType, out var handlers))
            {
                handlers.RaiseEvent(evt);
            }
        }
    }
}