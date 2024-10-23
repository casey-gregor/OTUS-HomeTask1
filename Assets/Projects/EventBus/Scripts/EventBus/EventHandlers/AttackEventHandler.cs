using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class AttackEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;

        public AttackEventHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void HandleEvent(AttackEvent evt)
        {
            Debug.Log("in Attack event handler");

            if (evt.Attacker.HealthComponent.IsDead) return;
            
            AttackVisualTask attackVisualTask = new AttackVisualTask(evt.Attacker, evt.Target);
            _eventBus.RaiseEvent(new AddVisualTaskEvent(attackVisualTask));
            RaiseDealDamageEvent(evt);
        }

        private void RaiseDealDamageEvent(AttackEvent evt)
        {
            int damage = evt.Damage != 0 ? evt.Damage : evt.Attacker.AttackDamage;

            _eventBus.RaiseEvent(new DealDamageEvent(evt.Attacker, evt.Target, damage));
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<AttackEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<AttackEvent>(HandleEvent);
        }
    }
}