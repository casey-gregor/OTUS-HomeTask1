using Atomic.Elements;
using Atomic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class BulletsObserveMechanics
    {

        private HashSet<Bullet> activeBullets = new();
        private AtomicVariable<Bullet> _newBullet;
        public BulletsObserveMechanics(AtomicEvent bulletShot, AtomicVariable<Bullet> newBullet, AtomicVariable<bool> isDead)
        {
            _newBullet = newBullet;
            bulletShot.Subscribe(AddToActiveBullets);
            isDead.Subscribe(value =>
            {
                if (value)
                {
                    StopAllBullets();
                }
            });

        }

        private void AddToActiveBullets()
        {
            Bullet bullet = _newBullet.Value;

            activeBullets.Add(bullet);

            IAtomicObservable<bool> IsActiveObservable = bullet.GetObservable<bool>(BulletAPIKeys.IS_DEAD);

            IsActiveObservable.Subscribe(bullet._core.InactiveHandler = value =>
            {
                IsActiveObservable.Unsubscribe(bullet._core.InactiveHandler);
                if (value)
                    RemoveFromActiveBullets(bullet);
            });

        }

        private void RemoveFromActiveBullets(Bullet bullet)
        {
            activeBullets.Remove(bullet);
        }

        private void StopAllBullets()
        {
            foreach (Bullet bullet in activeBullets)
            {
                bullet.GetVariable<bool>(BulletAPIKeys.CAN_MOVE).Value = false;
            }
        }
    }
}