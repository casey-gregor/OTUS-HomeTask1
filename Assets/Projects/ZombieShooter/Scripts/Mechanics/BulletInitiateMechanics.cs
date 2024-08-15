using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;


namespace ZombieShooter
{
    public class BulletInitiateMechanics
    {
        private IAtomicValue<Bullet> _bullet;
        private Transform _firePoint;
        private IAtomicAction<Bullet> _removeBulletAction;
        private IAtomicValue<LevelBounds> _levelBounds;

        public BulletInitiateMechanics(
            IAtomicValue<Bullet> bullet,
            Transform firePoint,
            IAtomicAction<Bullet> removeBulletAction,
            AtomicEvent BulletSpawnedEvent,
            IAtomicValue<LevelBounds> levelBounds
            )
        {
            _bullet = bullet;
            _firePoint = firePoint;
            _removeBulletAction = removeBulletAction;
            _levelBounds = levelBounds;
            
            BulletSpawnedEvent.Subscribe(InitiateBullet);

        }

        public void InitiateBullet()
        {
            Bullet bullet = _bullet.Value;

            bullet.LevelBounds.Value = _levelBounds.Value;
            
            bullet.transform.position = _firePoint.position;
            
            bullet.Initialize();

            bullet.GetVariable<Vector3>(BulletAPIKeys.MOVE_DIRECTION).Value = _firePoint.forward;

            bullet.GetVariable<bool>(BulletAPIKeys.IS_DEAD).Value = false;

            IAtomicObservable<bool> IsActiveObservable = bullet.GetObservable<bool>(BulletAPIKeys.IS_DEAD);

            IsActiveObservable.Subscribe(bullet.Core.InactiveHandler = value =>
            {
                IsActiveObservable.Unsubscribe(bullet.Core.InactiveHandler);
                if (value)
                    _removeBulletAction.Invoke(bullet);
            });

        }

    }
}