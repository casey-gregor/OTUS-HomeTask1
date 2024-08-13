using Atomic.Elements;
using Atomic.Extensions;
using System;
using UnityEngine;


namespace ZombieShooter
{
    public class BulletInitiateMechanics
    {
        IAtomicValue<Bullet> _bullet;
        Transform _firePoint;
        IAtomicAction<Bullet> _removeBulletAction;

        public BulletInitiateMechanics(
            IAtomicValue<Bullet> bullet, 
            Transform firePoint, 
            IAtomicAction<Bullet> removeBulletAction,
            AtomicEvent BulletSpawnedEvent)
        {
            _bullet = bullet;
            _firePoint = firePoint;
            _removeBulletAction = removeBulletAction;
            
            BulletSpawnedEvent.Subscribe(InitiateBullet);

        }

        public void InitiateBullet()
        {
            Bullet bullet = _bullet.Value;

            bullet.transform.position = _firePoint.position;

            bullet.GetVariable<Vector3>(APIKeys.MOVE_DIRECTION).Value = _firePoint.forward;

            bullet.GetVariable<bool>(APIKeys.IS_DEAD).Value = false;

            IAtomicObservable<bool> IsActiveObservable = bullet.GetObservable<bool>(APIKeys.IS_DEAD);

            IsActiveObservable.Subscribe(bullet.InactiveHandler = value =>
            {
                IsActiveObservable.Unsubscribe(bullet.InactiveHandler);
                if (value)
                    _removeBulletAction.Invoke(bullet);
            });

        }

    }
}