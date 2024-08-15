using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ZombieCore
    {
        [HideInInspector] public Action<bool> IsDeadHandler = null;
        [HideInInspector] public AtomicEvent<Zombie> DeadEvent;
        [HideInInspector] public Zombie Zombie;

        public RotationComponent RotationComponent;
        public MoveComponent MoveComponent;
        public LifeComponent LifeComponent;
        public MeleeAttackComponent AttackComponent;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private CheckAttackMechanics _checkAttackMechanics;

        public void Construct(Zombie zombie)
        {
            Zombie = zombie;

            var targetEntity = new AtomicFunction<AtomicEntity>(() =>
            {
                return AttackComponent.TargetObject.Value.GetComponent<AtomicEntity>(); ;
            });

            var isTargetAlive = new AtomicFunction<bool>(() =>
            {
                return !AttackComponent.TargetObject.Value.GetVariable<bool>(CharacterAPIKeys.IS_DEAD).Value;
            });

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return AttackComponent.TargetObject.Value.transform.position;

            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return AttackComponent.TargetObject.Value != null;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return zombie.transform.position;
            });

            

            MoveComponent.Construct();
            MoveComponent.AddCondition(LifeComponent.IsAlive);
            MoveComponent.AddCondition(isTargetAlive);

            RotationComponent.Construct();
            RotationComponent.AddCondition(LifeComponent.IsAlive);

            LifeComponent.Construct();
            AttackComponent.Construct();

            _lookAtTargetMechanics = new LookAtTargetMechanics(
                targetPosition,
                rootPosition,
                hasTarget,
                RotationComponent.RotateDirection,
                MoveComponent.MoveDirection);

            _checkAttackMechanics = new CheckAttackMechanics(
                rootPosition,
                targetEntity,
                MoveComponent.MoveDirection,
                AttackComponent.AttackDistance,
                AttackComponent.AttackRequestEvent,
                AttackComponent.DamageInterval);


            zombie.AddLogic(_lookAtTargetMechanics);
            zombie.AddLogic(MoveComponent);
            zombie.AddLogic(_checkAttackMechanics);
        }
    }
}