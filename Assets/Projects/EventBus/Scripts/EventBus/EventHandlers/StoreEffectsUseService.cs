using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class StoreEffectsUseService : IInitializable, ILateDisposable
    {
        public List<EffectsUsedData> Effects = new();
        
        private readonly EventBus _eventBus;
        public StoreEffectsUseService(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void HandleEvent(StoreEffectsDataEvent evt)
        {
            Debug.Log("store effect : " + evt.Effect);
            Effects.Add(new EffectsUsedData(evt.Invoker, evt.Effect, evt.Target));
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<StoreEffectsDataEvent>(HandleEvent);
        }


        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<StoreEffectsDataEvent>(HandleEvent);
        }

        public void ClearEffects()
        {
            Effects.Clear();
        }
    }
}