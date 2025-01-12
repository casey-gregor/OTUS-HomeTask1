using Atomic.Elements;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieShooter
{
    [Serializable]
    public class ShootComponent
    {
        public Transform firePoint;
        public AtomicVariable<LevelBounds> levelBounds;
        
        [HideInInspector] public AtomicEvent shootRequestEvent;
        [HideInInspector] public AtomicEvent shootActionEvent;
        [HideInInspector] public AtomicEvent fireEvent;
        [HideInInspector] public AtomicEvent bulletShot;
        [HideInInspector] public AtomicEvent bulletReloaded;

        [HideInInspector] public AtomicVariable<bool> isReloading;
        [HideInInspector] public AtomicVariable<Bullet> newBullet;
        public AtomicVariable<float> reloadTime;
        public AtomicVariable<int> bulletsInMagazine;

        public int initialBulletCount;
        public Bullet bulletPrefab;
        public Transform bulletParent;
        public Transform world;
        
        private bool _canFire = true;

        private CompositeCondition _condition = new();

        public void Construct()
        {
            
            shootRequestEvent.Subscribe(() =>
            {
                if (CanFire())
                {    
                    shootActionEvent.Invoke();
                }
            });

            fireEvent.Subscribe(Shoot);

        }

        public bool CanFire()
        {
            return _canFire && !isReloading.Value && _condition.IsTrue();
        }

        private void Shoot()
        {
            if (!CanFire())
                return;

            bulletShot?.Invoke();
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