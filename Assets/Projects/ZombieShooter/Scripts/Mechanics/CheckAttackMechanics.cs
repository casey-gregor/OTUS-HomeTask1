using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class CheckAttackMechanics : IAtomicUpdate
    {
        private IAtomicValue<Vector3> _rootPosition;
        private IAtomicValue<AtomicEntity> _target;
        private IAtomicVariable<Vector3> _moveDirection;
        private IAtomicValue<int> _attackDistance;
        private IAtomicAction AttackAction;
        private IAtomicValue<float> _damageInterval;

        private float _currentTimer;
        private bool _canDealDamage = true;

        public CheckAttackMechanics(
            IAtomicValue<Vector3> rootPosition,
            IAtomicValue<AtomicEntity> target,
            IAtomicVariable<Vector3> moveDirection,
            IAtomicValue<int> attackDistance,
            IAtomicAction attackAction,
            IAtomicValue<float> damageInterval)
        {
            _rootPosition = rootPosition;
            _target = target;
            _moveDirection = moveDirection;
            _attackDistance = attackDistance;
            AttackAction = attackAction;
            _damageInterval = damageInterval;
        }

        public void OnUpdate(float deltaTime)
        {
            TimerCountdown(deltaTime);

            float distanceToTarget = (_target.Value.transform.position - _rootPosition.Value).magnitude;
            if (distanceToTarget <= _attackDistance.Value && CanDamage(_target.Value))
            {
                _moveDirection.Value = Vector3.zero;

                AttackAction.Invoke();

                SetDamageTimer(_damageInterval.Value);
            }
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

        private void SetDamageTimer(float damageTimer)
        {
            _currentTimer = damageTimer;
            _canDealDamage = false;
        }

        private bool CanDamage(IAtomicEntity target)
        {
            if (!target.GetValue<bool>(CharacterAPIKeys.IS_DEAD).Value && _canDealDamage)
            {
                return true;
            }

            return false;
        }
    }
}