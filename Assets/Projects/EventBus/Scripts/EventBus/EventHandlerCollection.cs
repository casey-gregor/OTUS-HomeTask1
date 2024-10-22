using System;
using System.Collections.Generic;

namespace EventBus
{
    public class EventHandlerCollection<T> : IEventHandlerCollection
    {
        private readonly List<Delegate> _handlers = new();
        
        private int counter = -1;
        
        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            _handlers.Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            int handlerIndex = _handlers.IndexOf(handler);
            if (counter > handlerIndex)
            {
                counter--;
            }
            _handlers.RemoveAt(handlerIndex);
        }
        

        public void RaiseEvent<TEvent>(TEvent evt)
        {
            if (evt is T specificEvent)
            {
                for (counter = 0; counter < _handlers.Count; counter++)
                {
                    var action = (Action<T>)_handlers[counter];
                    action.Invoke(specificEvent);
                }
                counter = -1;
            }
           
            
        }
    }
}