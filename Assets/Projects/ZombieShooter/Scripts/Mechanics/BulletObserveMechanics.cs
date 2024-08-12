using Atomic.Elements;
using Atomic.Extensions;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class BulletObserveMechanics
    {
        private IAtomicValue<Bullet> _bullet;
        public BulletObserveMechanics(IAtomicValue<Bullet> bullet, IAtomicAction<Bullet> removeAction)
        {
            _bullet = bullet;
            _bullet.Value.GetObservable<bool>(APIKeys.IS_DEAD).Subscribe(result =>
            {
                if (result)
                {
                    removeAction.Invoke(_bullet.Value);
                }
            });
        }
    }
}