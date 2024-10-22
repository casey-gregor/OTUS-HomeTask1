using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class FreezeTargetEffectHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public FreezeTargetEffectHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEffect(FreezeTargetEffect effect)
        {
            Debug.Log("in FreezeTargetEffectHandler");
            if(effect.Source.TryGetEffect(out IEffect sourceEffect) && sourceEffect == effect)
            {
                effect.Target.ModifyTurnsToSkip(1);
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect, effect.Source, effect.Target));
            }
        }
        public void Initialize()
        {
            _eventBus.SubscribeHandler<FreezeTargetEffect>(HandleEffect);
        }


        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<FreezeTargetEffect>(HandleEffect);
        }
    }
}