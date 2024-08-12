using Atomic.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class RotationComponent
    {
        [HideInInspector] public AtomicVariable<Vector3> RotateDirection;

        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;

        private readonly CompositeCondition _condition = new CompositeCondition();

        public void Construct()
        {
            RotateDirection.Subscribe(Rotate);
        }

        public void Rotate(Vector3 direction)
        {
            RotateDirection.Value = direction;

            if (!_canRotate || !_condition.IsTrue())
            {
                return;
            }

            if (direction == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(RotateDirection.Value, Vector3.up);

            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

    }
}