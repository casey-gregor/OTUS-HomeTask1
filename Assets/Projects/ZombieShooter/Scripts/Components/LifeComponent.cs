using Atomic.Elements;
using System;
using UnityEngine;

namespace ZombieShooter

{
    [Serializable]
    public class LifeComponent
    {
        [HideInInspector] public AtomicEvent<int> DeductHitPointEvent;

        public AtomicVariable<bool> IsDead;
        public AtomicVariable<int> HitPoints;

        public void Construct()
        {
            DeductHitPointEvent.Subscribe(DeductHitPoints);

            HitPoints.Subscribe((hp) =>
            {
                if (hp <= 0)
                {
                    
                    IsDead.Value = true;
                }
            });

        }
        public bool IsAlive()
        {
            return !IsDead.Value;
        }

        public void DeductHitPoints(int amount)
        {
            if (IsDead.Value)
                return;

            HitPoints.Value -= amount;            
        }
    }
}
