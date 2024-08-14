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
        public AtomicVariable<LevelBounds> _levelBounds;
        
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
        
        private bool _canFire = true;

        private CompositeCondition _condition = new();

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
            return _canFire && !_isReloading.Value && _condition.IsTrue();
        }

        private void Shoot()
        {
            if (!CanFire())
                return;

            BulletShot?.Invoke();
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void AddCondition(AtomicFunction<bool> condition)
        {
            _condition.AddCondition(condition);
        }

    }
}