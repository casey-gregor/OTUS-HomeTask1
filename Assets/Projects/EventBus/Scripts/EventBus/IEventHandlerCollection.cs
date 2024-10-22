using System;

namespace EventBus
{
    public interface IEventHandlerCollection
    {
        public void Subscribe<TEvent>(Action<TEvent> handler);
        public void Unsubscribe<TEvent>(Action<TEvent> handler);
        public void RaiseEvent<TEvent>(TEvent evt);
    }
}