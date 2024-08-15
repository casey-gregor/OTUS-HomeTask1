using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class Bullet : AtomicEntity
    {
        public BulletCore Core;

        [HideInInspector] public AtomicVariable<LevelBounds> LevelBounds;

        [Get(BulletAPIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => Core._moveComponent.MoveDirection;

        [Get(BulletAPIKeys.CAN_MOVE)]
        public IAtomicVariable<bool> CanMove => Core._moveComponent.CanMove;

        [Get(BulletAPIKeys.DAMAGE)]
        public IAtomicVariable<int> Damage => Core._damage;

        [Get(BulletAPIKeys.IS_DEAD)]
        public IAtomicVariable<bool> IsDead => Core._isDead;


        public void Initialize()
        {
            Core.Construct(this);
        }

        private void Update()
        {
            Core._moveComponent.OnUpdate(Time.deltaTime);
            Core._inBoundsCheckMechanics.OnUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity entity))
            {
                entity.GetAction<int>(ZombieAPIKeys.DEDUCT_HITPOINTS).Invoke(Core._damage.Value);

                Core._isDead.Value = true;
            }
        }

    }

}
