using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class AvoidHitWhenAttackingHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        public AvoidHitWhenAttackingHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void HandleEffect(AvoidHitWhenAttackingEffect effect)
        {
            Debug.Log("in AvoidHitWhenAttacking EffectHandler");
            
            effect.Source.HealthComponent.SetInvincible(true);
            effect.RaisedSuccessfully = true;
            // _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
            
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<AvoidHitWhenAttackingEffect>(HandleEffect);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<AvoidHitWhenAttackingEffect>(HandleEffect);
        }
    }
}