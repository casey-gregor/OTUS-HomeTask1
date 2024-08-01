using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class LookAtTargetMechanics : IAtomicUpdate
    {
        private IAtomicValue<Vector3> _target;
        private IAtomicValue<Vector3> _root;
        private IAtomicValue<bool> _hasTarget;
        private IAtomicAction<Vector3> _rotationAction;
        private IAtomicAction<Vector3> _directionAction;

        public LookAtTargetMechanics(
            IAtomicValue<Vector3> target,
            IAtomicValue<Vector3> root,
            IAtomicValue<bool> hasTarget,
            IAtomicAction<Vector3> rotationAction,
            IAtomicAction<Vector3> directionAction)
        {
            _target = target;
            _root = root;
            _rotationAction = rotationAction;
            _hasTarget = hasTarget;
            _directionAction = directionAction;
            
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_hasTarget.Value)
                return;

            var direction = _target.Value - _root.Value;
            direction.y = 0;

            _rotationAction.Invoke(direction);
            _directionAction.Invoke(direction);
        }

    }
}