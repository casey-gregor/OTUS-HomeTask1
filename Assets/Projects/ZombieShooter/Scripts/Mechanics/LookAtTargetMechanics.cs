using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class LookAtTargetMechanics : IAtomicUpdate
    {
        private IAtomicValue<Vector3> _target;
        private IAtomicValue<Vector3> _root;
        private IAtomicValue<bool> _hasTarget;
        private IAtomicVariable<Vector3> _rotationDirection;
        private IAtomicVariable<Vector3> _moveDirection;

        public LookAtTargetMechanics(
            IAtomicValue<Vector3> target,
            IAtomicValue<Vector3> root,
            IAtomicValue<bool> hasTarget,
            IAtomicVariable<Vector3> rotationDirection,
            IAtomicVariable<Vector3> moveDirection)
        {
            _target = target;
            _root = root;
            _hasTarget = hasTarget;
            _rotationDirection = rotationDirection;
            _moveDirection = moveDirection;
            
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_hasTarget.Value)
                return;

            Vector3 direction = _target.Value - _root.Value;
            if(direction.magnitude < 1)
            {
                direction = Vector3.zero;
            }
            direction.y = 0;

            _rotationDirection.Value = direction;
            _moveDirection.Value = direction;
        }

    }
}