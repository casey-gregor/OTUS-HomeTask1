using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class Zombie : AtomicObject
    {
        public ZombieCore _core;

        [Get(ZombieAPIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core._moveComponent.MoveDirection;

        [Get(ZombieAPIKeys.TARGET)]
        public AtomicVariable<AtomicObject> Target => _core._targetObject;

        [Get(ZombieAPIKeys.IS_DEAD)]
        public AtomicVariable<bool> IsDead => _core._lifeComponent.isDead;

        [Get(ZombieAPIKeys.DAMAGE)]
        public AtomicVariable<int> Damage => _core._damageAmount;

        [Get(ZombieAPIKeys.DAMAGE_INTERVAL)]
        public AtomicVariable<float> DamageInterval => _core._damageInterval;

        [Get(ZombieAPIKeys.HIT_POINTS)]
        public IAtomicVariable<int> HitPoints => _core._lifeComponent._hitPoints;

        [Get(ZombieAPIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => _core._lifeComponent.DeductHitPointEvent;
      

        public void Awake()
        {
            _core.Construct(this);
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            OnFixedUpdate(fixedDeltaTime);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            OnUpdate(deltaTime);

        }

    }
}