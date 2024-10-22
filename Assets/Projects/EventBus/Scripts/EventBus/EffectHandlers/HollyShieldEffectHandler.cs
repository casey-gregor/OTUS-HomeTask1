using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class HollyShieldEffectHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        private int _numOfExecutionsAvailable;
        private bool _valueSet;

        public HollyShieldEffectHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEffect(HollyShieldEffect effect)
        {
            Debug.Log("in HollyShieldEffectHandler");
            if (!_valueSet)
            {
                _numOfExecutionsAvailable = effect.AvailableQty;
                _valueSet = true;
            }
            
            HeroEntity heroWithAbility = GetHeroWithAbility(effect);
            
            if (heroWithAbility.IsInvincible && _numOfExecutionsAvailable <= 0)
            {
                heroWithAbility.SetInvincible(false);
                return;
            }

            if (_numOfExecutionsAvailable > 0)
            {
                heroWithAbility.SetInvincible(true);
                _numOfExecutionsAvailable--;
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect, heroWithAbility, effect.Target));
                Debug.Log("Effect has been executed for " + heroWithAbility.View.name);
            }
            
        }

        private HeroEntity GetHeroWithAbility(HollyShieldEffect effect)
        {
            HeroEntity heroWithAbility;
            if (effect.Source.TryGetEffect(out IEffect sourceEffect) && sourceEffect == effect)
            {
                heroWithAbility = effect.Source;
            }
            else
            {
                heroWithAbility = effect.Target;
                effect.Source = heroWithAbility;
                effect.Target = effect.Source;
            }

            return heroWithAbility;
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<HollyShieldEffect>(HandleEffect);
        }
        
        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<HollyShieldEffect>(HandleEffect);
        }
    }
}