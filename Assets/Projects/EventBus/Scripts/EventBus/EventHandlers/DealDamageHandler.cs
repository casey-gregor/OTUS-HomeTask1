using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class DealDamageHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;

        public DealDamageHandler(EventBus eventBus, VisualPipeline visualPipeline)
        {
            _eventBus = eventBus;
            _visualPipeline = visualPipeline;
        }

        private void HandleEvent(DealDamageEvent evt)
        {
            ApplyDamage(evt.Target, evt.AttackerDamage);
            ApplyDamage(evt.Attacker, evt.Target.AttackDamage);
            
            _eventBus.RaiseEvent(new CheckIfDeadEvent(evt.Attacker));
            _eventBus.RaiseEvent(new CheckIfDeadEvent(evt.Target));
          
        }
        
        private void ApplyDamage(HeroEntity target, int damage)
        {
            if (target == null)
                return;
            
            if (target.IsInvincible)
            {
                target.SetInvincible(false);
                return;
            }
            
            _eventBus.RaiseEvent(new StoreAttackDataEvent(target, damage));
            target.DeductHealth(damage);
            _visualPipeline.AddGameTask(new SetStatsTask(null, target));
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