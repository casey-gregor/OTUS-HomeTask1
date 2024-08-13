using Atomic.Elements;
using Atomic.Extensions;
using System;
using System.Collections;
using UnityEngine;
using ZombieShooter;

namespace ZombieShooter
{
    public class BulletSpawnerMechanics
    {
        public AtomicEvent<Bullet> RemoveBulletEvent = new();
        public AtomicEvent BulletSpawned = new();

        private int _initialBulletCount;
        private Pool<Bullet> _bulletPool;
        private Bullet _bulletPrefab;
        private Transform _bulletParent;
        private Transform _world;
        private AtomicVariable<Bullet> _newBullet;

        public BulletSpawnerMechanics(
            int initialBulletCount, 
            Bullet bulletPrefab, 
            Transform bulletParent, 
            Transform world,
            AtomicVariable<Bullet> newBullet,
            AtomicEvent BulletShot)
        {
            _bulletPrefab = bulletPrefab;
            _initialBulletCount = initialBulletCount;
            _bulletParent = bulletParent;
            _world = world;
            _newBullet = newBullet;

            BulletShot.Subscribe(GetBullet);

            _bulletPool = new Pool<Bullet>(_bulletPrefab, _initialBulletCount, _bulletParent, _world);

            RemoveBulletEvent.Subscribe(RemoveBullet);
        }

        public void GetBullet()
        {
            _newBullet.Value = _bulletPool.GetObject();
            BulletSpawned?.Invoke();
        }

        public void RemoveBullet(Bullet bullet)
        {            
            _bulletPool.Enqueue(bullet);
        }

       
    }
}