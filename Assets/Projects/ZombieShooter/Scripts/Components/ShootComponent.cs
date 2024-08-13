using Atomic.Elements;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ShootComponent
    {
        public Transform _firePoint;
        
        [HideInInspector] public AtomicEvent ShootRequestEvent;
        [HideInInspector] public AtomicEvent ShootActionEvent;
        [HideInInspector] public AtomicEvent FireEvent;
        [HideInInspector] public AtomicEvent BulletShot;

        [HideInInspector] public AtomicVariable<bool> _isReloading;
        [HideInInspector] public AtomicVariable<Bullet> _newBullet;
        public AtomicVariable<float> _reloadTime;
        public AtomicVariable<int> _bulletsInMagazine;

        public int _initialBulletCount;
        public Bullet _bulletPrefab;
        public Transform _bulletParent;
        public Transform _world;

        
        [SerializeField] private bool _canFire;

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

        }

        public bool CanFire()
        {
            return _canFire && !_isReloading.Value;
        }

        private void Shoot()
        {
            if (!CanFire())
                return;

            BulletShot?.Invoke();
        }
     
    }
}