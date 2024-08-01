using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ShootComponent : IAtomicUpdate
    {
        public AtomicEvent ShootEvent;
        [SerializeField] private float _reloadTime = 2f;
        [SerializeField] private bool _isReloading;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private bool _canFire;

        private float _reloadTimer;

        private readonly CompositeCondition _condition = new CompositeCondition();

        public void Construct()
        {
            ShootEvent.Subscribe(Shoot);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isReloading)
            {
                _reloadTimer -= deltaTime;
                if (_reloadTimer <= 0)
                {
                    _isReloading = false;
                }
            }
        }

        public bool CanFire()
        {
            return _canFire;
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void Shoot()
        {
            if (!_canFire)
                return;
            if (_isReloading)
                return;

            var bullet = UnityEngine.Object.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.GetVariable<Vector3>(APIKeys.MoveDirection).Value = _firePoint.forward;

            _reloadTimer = _reloadTime;
            _isReloading = true;

            Debug.Log("fire");
        }
    }
}