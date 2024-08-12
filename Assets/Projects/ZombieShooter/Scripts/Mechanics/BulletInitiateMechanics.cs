using Atomic.Elements;
using Atomic.Extensions;
using System;
using UnityEngine;


namespace ZombieShooter
{
    public class BulletInitiateMechanics
    {
        public void InitiateBullet(Bullet _bullet, Transform _firePoint, IAtomicAction<Bullet> _removeBulletAction)
        {
            _bullet.transform.position = _firePoint.position;
            _bullet.GetVariable<Vector3>(APIKeys.MOVE_DIRECTION).Value = _firePoint.forward;

            _bullet.GetVariable<bool>(APIKeys.IS_ACTIVE).Value = true;

            IAtomicObservable<bool> IsActiveObservable = _bullet.GetObservable<bool>(APIKeys.IS_ACTIVE);

            IsActiveObservable.Subscribe(_bullet.InactiveHandler = value =>
            {
                IsActiveObservable.Unsubscribe(_bullet.InactiveHandler);
                if (!value)
                    _removeBulletAction.Invoke(_bullet);
            });
 
        }

    }
}