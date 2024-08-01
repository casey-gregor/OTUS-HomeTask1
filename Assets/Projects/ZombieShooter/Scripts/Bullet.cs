using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class Bullet : AtomicEntity
    {
        [Get(APIKeys.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => _moveComponent.MoveDirection;

        [SerializeField] private MoveComponent _moveComponent;

        [SerializeField] private int _damage = 1;

       
        private void Update()
        {
            _moveComponent.OnUpdate(Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity entity))
            {
                entity.GetAction<int>(APIKeys.TakeDamageAction).Invoke(_damage);
            }
        }
    }

}
