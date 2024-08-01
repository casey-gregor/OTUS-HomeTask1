using Atomic.Elements;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class ShootTargetMechanics
    {
        private IAtomicValue<Vector3> _target;
        private IAtomicValue<Vector3> _root;
        private AtomicVariable<float> _radius;
        private IAtomicValue<bool> _hasTarget;
        private IAtomicAction _shootAction;

        public ShootTargetMechanics(
            IAtomicValue<Vector3> targetPoint,
            IAtomicValue<Vector3> root,
            AtomicVariable<float> radius,
            IAtomicValue<bool> hasTarget,
            IAtomicAction shootComponent)
        {
            _target = targetPoint;
            _root = root;
            _radius = radius;
            this._hasTarget = hasTarget;
            _shootAction = shootComponent;
        }

        public void OnUpdate()
        {
            if (!_hasTarget.Value) return;

            var direction = _target.Value - _root.Value;

            if (direction.sqrMagnitude < _radius.Value)
            {
                _shootAction.Invoke();
            }
        }
    }
}