using Atomic.Elements;
using Atomic.Objects;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterCore
    {

        [SerializeField] private Camera _camera;

        [SerializeField] public MoveComponent MoveComponent;
        public RotationComponent RotationComponent;
        public LifeComponent LifeComponent;
        public ShootComponent ShootComponent;

        [SerializeField] private AtomicVariable<GameObject> _targetObject;

        private RotateOnMouseCoursorMechanics _rotateOnMouseCoursorMechanics;

        public void Construct(Character character)
        {
            MoveComponent.Construct();
            RotationComponent.Construct();
            ShootComponent.Construct();
            LifeComponent.Construct();

            MoveComponent.AddCondition(LifeComponent.IsAlive);
            MoveComponent.AddCondition(ShootComponent.CanFire);
            RotationComponent.AddCondition(LifeComponent.IsAlive);

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _targetObject.Value.transform.position;
            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetObject != null;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return character.transform.position;
            });



            _rotateOnMouseCoursorMechanics = new RotateOnMouseCoursorMechanics(_camera, rootPosition, character);

            character.AddLogic(_rotateOnMouseCoursorMechanics);
            character.AddLogic(MoveComponent);
            character.AddLogic(ShootComponent);

        }

        public void TakeDamage(int damage)
        {
            LifeComponent.TakeDamage(damage);
        }

    }
}