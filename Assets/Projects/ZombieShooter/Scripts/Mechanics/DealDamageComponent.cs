using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class DealDamageComponent : IAtomicUpdate
    {
        public AtomicEvent DealDamageEvent;

        [SerializeField] private AtomicEntity _target;
        [SerializeField] private AtomicVariable<int> _damage;
        [SerializeField] private AtomicVariable<int> _timeBetweenHits;


        private float _currentTimer;
        private bool _canDealDamage;


        public void Construct(IAtomicValue<AtomicEntity> target)
        {
            _target = target.Value;
            _canDealDamage = true;

            DealDamageEvent.Subscribe(DealDamage);
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_canDealDamage)
            {
                _currentTimer -= deltaTime;
                if (_currentTimer <= 0)
                {
                    _canDealDamage = true;
                }
            }
        }

        private void DealDamage()
        {
            if(_canDealDamage && !_target.GetValue<bool>(APIKeys.IS_DEAD).Value)
            {
                _target.GetAction<int>(APIKeys.DEDUCT_HITPOINTS).Invoke(_damage.Value);

                _currentTimer = _timeBetweenHits.Value;
                _canDealDamage = false;

            }
        }


    }
}