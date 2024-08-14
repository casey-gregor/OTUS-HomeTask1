using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class MeleeAttackMechanics : IAtomicUpdate
    {
        private IAtomicValue<AtomicObject> _targetObject;
        private IAtomicValue<Vector3> _rootPosition;
        private IAtomicVariable<bool> _canMove;
        private IAtomicValue<float> _damageTimer;
        private IAtomicValue<int> _damageAmount;

        private float _currentTimer;
        private bool _canDealDamage = true;


        public MeleeAttackMechanics(
            IAtomicValue<AtomicObject> targetObject, 
            IAtomicValue<Vector3> rootPosition, 
            IAtomicVariable<bool> canMove,
            IAtomicValue<float> damageTimer,
            IAtomicValue<int> damageAmount)
        {
            _targetObject = targetObject;
            _rootPosition = rootPosition;
            _canMove = canMove;
            _damageTimer = damageTimer;
            _damageAmount = damageAmount;

        }

        public void OnUpdate(float deltaTime)
        {
            if(_targetObject.Value != null)
            {
                float distanceToTarget = (_targetObject.Value.transform.position - _rootPosition.Value).magnitude;
                if (distanceToTarget <= 1)
                {
                    _canMove.Value = false;
                    Attack(_targetObject.Value, _damageAmount.Value);
                }
                else
                {
                    _canMove.Value = true;
                }
            }

            TimerCountdown(deltaTime);
        }

        public void Attack(AtomicObject target, int damageAmount)
        {

            if (CanDamage(target))
            {
                target.GetAction<int>(CharacterAPIKeys.DEDUCT_HITPOINTS).Invoke(damageAmount);

                SetDamageTimer(_damageTimer.Value);
            }
        }

        private bool CanDamage(IAtomicEntity target)
        {
            if (!target.GetValue<bool>(CharacterAPIKeys.IS_DEAD).Value && _canDealDamage)
            {
                return true;
            }

            return false;
        }

        private void SetDamageTimer(float damageTimer)
        {
            _currentTimer = damageTimer;
            _canDealDamage = false;
        }

        private void TimerCountdown(float deltaTime)
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
    }
}