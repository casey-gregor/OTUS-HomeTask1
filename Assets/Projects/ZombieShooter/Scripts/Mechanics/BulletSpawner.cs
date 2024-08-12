using Atomic.Elements;
using Atomic.Extensions;
using System;
using System.Collections;
using UnityEngine;
using ZombieShooter;

namespace ZombieShooter
{
    public class BulletSpawner
    {
        public AtomicEvent<Bullet> RemoveBulletEvent = new();

        private int _initialBulletCount;
        private Pool<Bullet> _bulletPool;
        private Bullet _bulletPrefab;
        private Transform _bulletParent;
        private Transform _world;

        public BulletSpawner(Bullet bulletPrefab, int initialBulletCount, Transform bulletParent, Transform world)
        {
            _bulletPrefab = bulletPrefab;
            _initialBulletCount = initialBulletCount;
            _bulletParent = bulletParent;
            _world = world;

            _bulletPool = new Pool<Bullet>(_bulletPrefab, _initialBulletCount, _bulletParent, _world);

            RemoveBulletEvent.Subscribe(RemoveBullet);
        }

        public Bullet GetBullet()
        {
            return _bulletPool.GetObject();
        }

        public void RemoveBullet(Bullet bullet)
        {            
            _bulletPool.Enqueue(bullet);
        }

       
    }
}