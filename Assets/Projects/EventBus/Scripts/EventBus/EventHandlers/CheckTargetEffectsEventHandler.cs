using Zenject;

namespace EventBus
{
    public sealed class CheckTargetEffectsEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public CheckTargetEffectsEventHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(CheckTargetEffectsEvent evt)
        {
            if(evt.Target.AbilityComponent.TryGetEffect(out IEffect targetEffect) 
               && targetEffect.Type == evt.Type)
            {
                targetEffect.Source = evt.Target;
                targetEffect.Target = evt.Attacker;
                
                _eventBus.RaiseEvent(targetEffect);
                RaiseStoreEffectEvent(targetEffect);
            }
        }
        private void RaiseStoreEffectEvent(IEffect effect)
        {
            if(effect.RaisedSuccessfully)
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<CheckTargetEffectsEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<CheckTargetEffectsEvent>(HandleEvent);
        }
    }
    
}