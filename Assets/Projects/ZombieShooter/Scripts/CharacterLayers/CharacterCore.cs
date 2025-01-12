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
        
        private BulletCounterPresenter _bulletCounterPresenter;

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
                ShootComponent.bulletsInMagazine, 
                ShootComponent.reloadTime,
                ShootComponent.isReloading,
                ShootComponent.bulletShot,
                ShootComponent.bulletReloaded);

            _bulletSpawnerMechanics = new BulletSpawnerMechanics(
                ShootComponent.initialBulletCount,
                ShootComponent.bulletPrefab,
                ShootComponent.bulletParent,
                ShootComponent.world,
                ShootComponent.newBullet,
                ShootComponent.bulletShot);

            _bulletInitiateMechanics = new BulletInitiateMechanics(
                ShootComponent.newBullet,
                ShootComponent.firePoint,
                _bulletSpawnerMechanics.RemoveBulletEvent,
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent.levelBounds);

            _bulletsObserveMechanics = new BulletsObserveMechanics(
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent.newBullet,
                LifeComponent.IsDead);

            _bulletCounterPresenter = new BulletCounterPresenter(
                ShootComponent.bulletShot,
                ShootComponent.bulletReloaded);


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