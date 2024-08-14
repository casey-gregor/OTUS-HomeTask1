using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using System.Collections;
using UnityEngine;
using ZombieShooter;

namespace ZombieShooter
{
    [Serializable]
    public class ZombieCore
    {

        public RotationComponent _rotationComponent;
        public MoveComponent _moveComponent;
        public LifeComponent _lifeComponent;

        public AtomicVariable<AtomicObject> _targetObject;
        public AtomicVariable<int> _damageAmount;
        public AtomicVariable<float> _damageInterval;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private MeleeAttackMechanics _attackMechanics;

        [HideInInspector] public Action<bool> isDeadHandler = null;

        public void Construct(Zombie zombie)
        {
            var targetEntity = new AtomicFunction<AtomicEntity>(() =>
            {
                return _targetObject.Value.GetComponent<AtomicEntity>(); ;
            });

            var isTargetAlive = new AtomicFunction<bool>(() =>
            {
                return !_targetObject.Value.GetVariable<bool>(ZombieAPIKeys.IS_DEAD).Value;
            });

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _targetObject.Value.transform.position;

            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetObject.Value != null;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return zombie.transform.position;
            });

            _moveComponent.AddCondition(_lifeComponent.IsAlive);
            _moveComponent.AddCondition(isTargetAlive);

            _rotationComponent.Construct();
            _rotationComponent.AddCondition(_lifeComponent.IsAlive);

            _lifeComponent.Construct();


            _lookAtTargetMechanics = new LookAtTargetMechanics(
                targetPosition,
                rootPosition,
                hasTarget,
                _rotationComponent.RotateDirection,
                _moveComponent.MoveDirection);

            _attackMechanics = new MeleeAttackMechanics(
                _targetObject,
                rootPosition,
                _moveComponent.CanMove,
                _damageInterval,
                _damageAmount);


            zombie.AddLogic(_lookAtTargetMechanics);
            zombie.AddLogic(_moveComponent);
            zombie.AddLogic(_attackMechanics);
        }
    }
}