using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ShootComponent : IAtomicUpdate
    {
        public Transform _firePoint;
        
        [HideInInspector] public AtomicEvent ShootRequestEvent;
        [HideInInspector] public AtomicEvent ShootActionEvent;
        [HideInInspector] public AtomicEvent FireEvent;

        [SerializeField] private bool _canFire;
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private int _bulletsInMagazine = 10;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _initialBulletCount;
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private Transform _world;

        private float _reloadTimer;
        private float _bulletsCount;
        private bool _isReloading;

        private BulletSpawnerMechanics _spawnerMechanics;
        private BulletInitiateMechanics _initiateMechanics;

        public void Construct()
        {
            
            ShootRequestEvent.Subscribe(() =>
            {
                if (CanFire())
                {
                    ShootActionEvent.Invoke();
                }
            });

            FireEvent.Subscribe(Shoot);

            _bulletsCount = _bulletsInMagazine;

            _spawnerMechanics = new BulletSpawnerMechanics(_bulletPrefab, _initialBulletCount, _bulletParent, _world);
            _initiateMechanics = new BulletInitiateMechanics();
        }

        public void OnUpdate(float deltaTime)
        {
            CheckIfReloading();

            RefillBulletMagazine();
        }

        public bool CanFire()
        {
            return _canFire && !_isReloading;
        }

        private void Shoot()
        {
            if (!CanFire())
                return;

            _reloadTimer = _reloadTime;
            _bulletsCount --;

            Bullet bullet = _spawnerMechanics.GetBullet();

            _initiateMechanics.InitiateBullet(bullet, _firePoint, _spawnerMechanics.RemoveBulletEvent);
        }
     

        private void RefillBulletMagazine()
        {
            if(_bulletsCount < _bulletsInMagazine)
            {
                _reloadTimer -= Time.deltaTime;
                if(_reloadTimer <= 0 )
                {
                    _bulletsCount++;
                    _reloadTimer = _reloadTime;
                }
            }

        }

        private void CheckIfReloading()
        {
            if (_bulletsCount <= 0)
            {  
                _isReloading = true;
            }
            else
            {
                _isReloading = false;
            }

        }
    }
}