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
            effect.Target.TurnComponent.EditSkipTurns(effect.SkipTurnNum);
            effect.RaisedSuccessfully = true;
            // _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
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