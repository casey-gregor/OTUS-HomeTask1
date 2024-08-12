using Atomic.Elements;
using System;
using UnityEngine;

namespace ZombieShooter

{
    [Serializable]
    public class LifeComponent
    {
        [HideInInspector] public AtomicEvent<int> DeductHitPointEvent;
        [HideInInspector] public AtomicVariable<bool> isDead;

        public AtomicVariable<int> _hitPoints;

        public void Construct()
        {
            DeductHitPointEvent.Subscribe(DeductHitPoints);

            _hitPoints.Subscribe((hp) =>
            {
                if (hp <= 0)
                {
                    
                    isDead.Value = true;
                }
            });

        }
        public bool IsAlive()
        {
            return !isDead.Value;
        }

        public void DeductHitPoints(int amount)
        {
            if (isDead.Value)
                return;

            _hitPoints.Value -= amount;            
        }
    }
}
