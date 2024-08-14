using Atomic.Elements;
using Atomic.Extensions;
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
        [SerializeField] private ZombieSpawnController _zombieSpawnController;

        public MoveComponent MoveComponent;
        public RotationComponent RotationComponent;
        public LifeComponent LifeComponent;
        public ShootComponent ShootComponent;

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
                if (_zombieSpawnController._zombiesAlive.Value <= 0)
                    return false;

                return true;
            });

            _rotateOnMouseCoursorMechanics = new RotateOnMouseCoursorMechanics(
                _camera, 
                rootPosition, 
                character);

            _bulletCountMechanics = new BulletCountMechanics(
                ShootComponent._bulletsInMagazine, 
                ShootComponent._reloadTime,
                ShootComponent._isReloading,
                ShootComponent.BulletShot);

            _bulletSpawnerMechanics = new BulletSpawnerMechanics(
                ShootComponent._initialBulletCount,
                ShootComponent._bulletPrefab,
                ShootComponent._bulletParent,
                ShootComponent._world,
                ShootComponent._newBullet,
                ShootComponent.BulletShot);

            _bulletInitiateMechanics = new BulletInitiateMechanics(
                ShootComponent._newBullet,
                ShootComponent._firePoint,
                _bulletSpawnerMechanics.RemoveBulletEvent,
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent._levelBounds);

            _bulletsObserveMechanics = new BulletsObserveMechanics(
                _bulletSpawnerMechanics.BulletSpawned,
                ShootComponent._newBullet,
                LifeComponent.isDead);




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