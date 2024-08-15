using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class BulletCountMechanics : IAtomicUpdate
    {
        private IAtomicValue<int> _bulletsInMagazine;
        private IAtomicValue<float> _reloadTime;
        private AtomicVariable<bool> _isReloading;

        private int _bulletsCount;
        private float _reloadTimer;

        public BulletCountMechanics(
            IAtomicValue<int> bulletsInMagazine, 
            IAtomicValue<float> reloadTime,
            AtomicVariable<bool> isReloading,
            AtomicEvent bulletShot)
        {
            _bulletsCount = bulletsInMagazine.Value;
            _bulletsInMagazine = bulletsInMagazine;
            _reloadTime = reloadTime;
            _isReloading = isReloading;

            bulletShot.Subscribe(DeductBullet);
        }

        public void OnUpdate(float deltaTime)
        {
            CheckIfReloading();

            RefillBulletMagazine();
        }

        private void RefillBulletMagazine()
        {
            if (_bulletsCount < _bulletsInMagazine.Value)
            {
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer <= 0)
                {
                    _bulletsCount++;
                    _reloadTimer = _reloadTime.Value;
                }
            }
        }

        private void CheckIfReloading()
        {
            if (_bulletsCount <= 0)
            {
                _isReloading.Value = true;
            }
            else
            {
                _isReloading.Value = false;
            }

        }

        private void DeductBullet()
        {
            _reloadTimer = _reloadTime.Value;
            _bulletsCount--;
        }

    }
}