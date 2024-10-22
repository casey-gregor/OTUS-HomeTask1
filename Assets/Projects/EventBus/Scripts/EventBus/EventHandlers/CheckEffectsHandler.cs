using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public class CheckEffectsHandler: IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public CheckEffectsHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(CheckEffectsEvent evt)
        {
            if (evt.CurrentHero.TryGetEffect(out IEffect effect) && 
                (effect.Type == evt.Type || effect.Type == EffectType.Any))
            {
                effect.Source = evt.CurrentHero;
                effect.Target = evt.Target;
                _eventBus.RaiseEvent(effect);

                evt.Target = effect.Target;
            }
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<CheckEffectsEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<CheckEffectsEvent>(HandleEvent);
        }
        
    }
}