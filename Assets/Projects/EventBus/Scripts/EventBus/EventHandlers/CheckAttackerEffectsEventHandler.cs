using Zenject;

namespace EventBus
{
    public sealed class CheckAttackerEffectsEventHandler: IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public CheckAttackerEffectsEventHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(CheckAttackerEffectsEvent evt)
        {
            HeroEntity finalTarget  = evt.Target;

            if (TryRaiseEffect(evt, out HeroEntity updatedTarget))
            {
                finalTarget = updatedTarget;
            }
            
            _eventBus.RaiseEvent(new CheckTargetEffectsEvent(evt.Attacker, finalTarget, EffectType.Any));
            _eventBus.RaiseEvent(new AttackEvent(evt.Attacker, finalTarget));
            
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<CheckAttackerEffectsEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<CheckAttackerEffectsEvent>(HandleEvent);
        }
        
        private bool TryRaiseEffect(CheckAttackerEffectsEvent evt, out HeroEntity updatedTarget)
        {
            updatedTarget = evt.Target;

            if (!evt.Attacker.AbilityComponent.TryGetEffect(out IEffect attackerEffect)
                || evt.Type != EffectType.Offensive) return false;
            
            attackerEffect.Source = evt.Attacker;
            attackerEffect.Target = evt.Target;
                
            _eventBus.RaiseEvent(attackerEffect);
            RaiseStoreEffectEvent(attackerEffect);
            updatedTarget = attackerEffect.Target;
            return true;
        }

        private void RaiseStoreEffectEvent(IEffect effect)
        {
            if(effect.RaisedSuccessfully)
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
        }
    }
}