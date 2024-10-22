using UnityEngine;
using Zenject;

namespace EventBus
{
    public class PreAttackCheckHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public PreAttackCheckHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(PreAttackCheckEvent evt)
        {
            _eventBus.RaiseEvent(new CheckAttackerEffectsEvent(evt.Attacker, evt.Target, EffectType.Offensive));
            // CheckEffectsEvent checkAttackerEffectsEvent = new CheckEffectsEvent
            //     (evt.Attacker, evt.Target, EffectType.Offensive);
            // _eventBus.RaiseEvent(checkAttackerEffectsEvent);
            //
            // HeroEntity finalTarget = checkAttackerEffectsEvent.Target;
            //
            // _eventBus.RaiseEvent(new CheckEffectsEvent(finalTarget, evt.Attacker, EffectType.Defensive));
            //
            // _eventBus.RaiseEvent(new AttackEvent(evt.Attacker, finalTarget));
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<PreAttackCheckEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<PreAttackCheckEvent>(HandleEvent);
        }
    }
}