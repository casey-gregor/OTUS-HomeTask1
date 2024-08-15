using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class Zombie : AtomicObject
    {
        public ZombieCore Core;
        public ZombieAnimation Animation;


        [Get(ZombieAPIKeys.TARGET)]
        public AtomicVariable<AtomicObject> Target => Core.AttackComponent.TargetObject;

        [Get(ZombieAPIKeys.IS_DEAD)]
        public AtomicVariable<bool> IsDead => Core.LifeComponent.IsDead;

        [Get(ZombieAPIKeys.DEAD_EVENT)]
        public AtomicEvent<Zombie> DeadEvent => Core.DeadEvent;

        [Get(ZombieAPIKeys.HIT_POINTS)]
        public AtomicVariable<int> HitPoints => Core.LifeComponent.HitPoints;

        [Get(ZombieAPIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => Core.LifeComponent.DeductHitPointEvent;


        public void Awake()
        {
            Core.Construct(this);
            Animation.Construct(Core);
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