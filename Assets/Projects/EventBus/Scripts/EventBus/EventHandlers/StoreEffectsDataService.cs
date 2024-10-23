using System.Collections.Generic;
using Zenject;

namespace EventBus
{
    public sealed class StoreEffectsDataService : IInitializable, ILateDisposable
    {
        public List<IEffect> Effects = new();
        
        private readonly EventBus _eventBus;
        public StoreEffectsDataService(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void HandleEvent(StoreEffectsDataEvent evt)
        {
            Effects.Add(evt.Effect);
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<StoreEffectsDataEvent>(HandleEvent);
        }
        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<StoreEffectsDataEvent>(HandleEvent);
        }
    }
}