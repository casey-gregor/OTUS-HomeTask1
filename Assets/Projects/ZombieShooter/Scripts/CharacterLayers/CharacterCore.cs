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

        public MoveComponent MoveComponent;
        public RotationComponent RotationComponent;
        public LifeComponent LifeComponent;
        public ShootComponent ShootComponent;

        private RotateOnMouseCoursorMechanics _rotateOnMouseCoursorMechanics;
        private BulletCountMechanics _bulletCountMechanics;
        private BulletSpawnerMechanics _bulletSpawnerMechanics;
        private BulletInitiateMechanics _bulletInitiateMechanics;

        public void Construct(Character character)
        {
            MoveComponent.Construct();
            RotationComponent.Construct();
            ShootComponent.Construct();
            LifeComponent.Construct();

            MoveComponent.AddCondition(LifeComponent.IsAlive);
            MoveComponent.AddCondition(ShootComponent.CanFire);
            RotationComponent.AddCondition(LifeComponent.IsAlive);

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return character.transform.position;
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
                _bulletSpawnerMechanics.BulletSpawned);

            character.AddLogic(_rotateOnMouseCoursorMechanics);
            character.AddLogic(MoveComponent);
            character.AddLogic(_bulletCountMechanics);
        }
    }
}