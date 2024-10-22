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

            if (effect.Source.TryGetEffect(out IEffect sourceEffect) && sourceEffect == effect)
            {
                Debug.Log("effect.Source : " + effect.Source.View.name);
                effect.Source.SetInvincible(true);
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect, effect.Source, effect.Target));
            }
            // else
            // {
            //     effect.Target.SetInvincible(false);
            // }

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