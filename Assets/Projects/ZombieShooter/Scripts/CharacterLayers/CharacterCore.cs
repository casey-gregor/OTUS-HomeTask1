using Atomic.Elements;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterCore
    {
        public MoveComponent MoveComponent;
        public RotationComponent RotationComponent;
        public LifeComponent LifeComponent;
        public ShootComponent ShootComponent;

        [SerializeField] private Camera _camera;
        [SerializeField] private ZombieSpawnController _zombieSpawnController;

        private RotateOnMouseCoursorMechanics _rotateOnMouseCoursorMechanics;
        private BulletCountMechanics _bulletCountMechanics;
        private BulletSpawnerMechanics _bulletSpawnerMechanics;
        private BulletInitiateMechanics _bulletInitiateMechanics;
        private BulletsObserveMechanics _bulletsObserveMechanics;

        public void Construct(Character character)
        {

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return character.transform.position;
            });

            var zombiesAlive = new AtomicFunction<bool>(() =>
            {
                if (_zombieSpawnController.ZombiesAlive.Value <= 0)
                    return false;

                return true;
            });

            _rotateOnMouseCoursorMechanics = new RotateOnMouseCoursorMechanics(
                _camera, 
                rootPosition, 
                character);

            _bulletCountMechanics = new BulletCountMechanics(
                ShootComponent.BulletsInMagazine, 
                ShootComponent.ReloadTime,
                ShootComponent.IsReloading,
                ShootComponent.BulletShot);

            _bulletSpawnerMechanics = new BulletSpawnerMechanics(
                ShootComponent.InitialBulletCount,
                ShootComponent.BulletPrefab,
                ShootComponent.BulletParent,
                ShootComponent.World,
                ShootComponent.NewBullet,
                ShootComponent.BulletShot);

            _bulletInitiateMechanics = new BulletInitiateMechanics(
                ShootComponent.NewBullet,
                ShootComponent.FirePoint,
                _bulletSpawnerMechanics.RemoveBulletEvent,
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent.LevelBounds);

            _bulletsObserveMechanics = new BulletsObserveMechanics(
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent.NewBullet,
                LifeComponent.IsDead);


            MoveComponent.Construct();
            RotationComponent.Construct();
            ShootComponent.Construct();
            LifeComponent.Construct();

            MoveComponent.AddCondition(LifeComponent.IsAlive);
            MoveComponent.AddCondition(zombiesAlive);

            RotationComponent.AddCondition(LifeComponent.IsAlive);
            RotationComponent.AddCondition(zombiesAlive);

            ShootComponent.AddCondition(zombiesAlive);

            character.AddLogic(_rotateOnMouseCoursorMechanics);
            character.AddLogic(MoveComponent);
            character.AddLogic(_bulletCountMechanics);
        }
    }
}