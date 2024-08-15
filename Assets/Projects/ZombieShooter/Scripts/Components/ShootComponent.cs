using Atomic.Elements;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ShootComponent
    {
        public Transform FirePoint;
        public AtomicVariable<LevelBounds> LevelBounds;
        
        [HideInInspector] public AtomicEvent ShootRequestEvent;
        [HideInInspector] public AtomicEvent ShootActionEvent;
        [HideInInspector] public AtomicEvent FireEvent;
        [HideInInspector] public AtomicEvent BulletShot;

        [HideInInspector] public AtomicVariable<bool> IsReloading;
        [HideInInspector] public AtomicVariable<Bullet> NewBullet;
        public AtomicVariable<float> ReloadTime;
        public AtomicVariable<int> BulletsInMagazine;

        public int InitialBulletCount;
        public Bullet BulletPrefab;
        public Transform BulletParent;
        public Transform World;
        
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
            return _canFire && !IsReloading.Value && _condition.IsTrue();
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