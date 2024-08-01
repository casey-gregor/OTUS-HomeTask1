using Atomic.Elements;
using Atomic.Objects;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class MoveComponent : IAtomicUpdate
    {
        public AtomicEvent<Vector3> DirectionEvent;

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private bool _canMove;

        public AtomicVariable<Vector3> MoveDirection;

        private readonly CompositeCondition _condition = new CompositeCondition();
        
        public void Construct()
        {
            DirectionEvent.Subscribe(SetDirection);
        }

        private void SetDirection(Vector3 vector)
        {
            MoveDirection.Value = vector;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.IsTrue() && _canMove)
            {
                _root.position += MoveDirection.Value * _speed * deltaTime;
            }
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

    }
}