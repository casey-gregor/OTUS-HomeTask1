using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class MoveComponent : IAtomicUpdate
    {
        [HideInInspector] public AtomicVariable<bool> CanMove;
        [HideInInspector] public AtomicVariable<Vector3> MoveDirection;
        [HideInInspector] public AtomicVariable<bool> IsMoving;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;

        private readonly CompositeCondition _condition = new CompositeCondition();
        
        public void Construct()
        {
            MoveDirection.Subscribe(direction =>
            {
                IsMoving.Value = direction.sqrMagnitude > 0;
            });
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.IsTrue() && CanMove.Value)
            {
                _root.position += MoveDirection.Value.normalized * _speed * deltaTime;

            }
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

    }
}