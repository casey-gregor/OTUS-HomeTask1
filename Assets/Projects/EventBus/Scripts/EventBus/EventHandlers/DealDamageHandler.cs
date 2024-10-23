using Zenject;

namespace EventBus
{
    public sealed class DealDamageHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public DealDamageHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(DealDamageEvent evt)
        {
            if (evt.Target != null)
            {
                ApplyDamage(evt.Target, evt.AttackerDamage);
                _eventBus.RaiseEvent(new CheckIfDeadEvent(evt.Target));
            }
            
            if (evt.Attacker != null)
            {
                ApplyDamage(evt.Attacker, evt.Target.AttackDamage);
                _eventBus.RaiseEvent(new CheckIfDeadEvent(evt.Attacker));
            }
            
        }
        
        private void ApplyDamage(HeroEntity target, int damage)
        {
            if (target.HealthComponent.IsInvincible)
            {
                target.HealthComponent.SetInvincible(false);
                return;
            }
            
            _eventBus.RaiseEvent(new StoreAttackDataEvent(target, damage));
            target.HealthComponent.DeductHealth(damage);
            
            SetStatsTask setStatsTask = new SetStatsTask(null, target);
            _eventBus.RaiseEvent(new AddVisualTaskEvent(setStatsTask));
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<DealDamageEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<DealDamageEvent>(HandleEvent);
        }
    }
}