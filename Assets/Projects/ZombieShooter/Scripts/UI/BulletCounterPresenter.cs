using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class BulletCounterPresenter
    {
        
        public BulletCounterPresenter(AtomicEvent bulletShot, AtomicEvent bulletReloaded)
        {
            bulletShot.Subscribe(DeductBullet);
            bulletReloaded.Subscribe(AddBullet);
        }

        private void AddBullet()
        {
            // Debug.Log("add one bullet from ui");
        }

        private void DeductBullet()
        {
            // Debug.Log("remove one bullet from ui");
        }
    }
}