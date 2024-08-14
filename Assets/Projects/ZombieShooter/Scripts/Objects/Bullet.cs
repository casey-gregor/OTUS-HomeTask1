using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;
using Zenject;

namespace ZombieShooter
{
    public class Bullet : AtomicEntity
    {
        public BulletCore _core;

        [HideInInspector] public AtomicVariable<LevelBounds> _levelBounds;

        [Get(BulletAPIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core._moveComponent.MoveDirection;

        [Get(BulletAPIKeys.CAN_MOVE)]
        public IAtomicVariable<bool> CanMove => _core._moveComponent.CanMove;

        [Get(BulletAPIKeys.DAMAGE)]
        public IAtomicVariable<int> Damage => _core._damage;

        [Get(BulletAPIKeys.IS_DEAD)]
        public IAtomicVariable<bool> IsDead => _core._isDead;


        public void Initialize()
        {
            _core.Construct(this);
        }

        private void Update()
        {
            _core._moveComponent.OnUpdate(Time.deltaTime);
            _core._inBoundsCheckMechanics.OnUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity entity))
            {
                entity.GetAction<int>(ZombieAPIKeys.DEDUCT_HITPOINTS).Invoke(_core._damage.Value);

                _core._isDead.Value = true;
            }
        }

    }

}
