using Atomic.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter

{
    [Serializable]
    public class LifeComponent
    {
        public AtomicEvent<int> TakeDamageEvent;

        [SerializeField] private AtomicVariable<int> _hitPoints;
        [SerializeField] public AtomicVariable<bool> isDead;

        public void Construct()
        {
            TakeDamageEvent.Subscribe(TakeDamage);

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

        public void TakeDamage(int damage)
        {
            if (isDead.Value)
                return;

            _hitPoints.Value -= damage;
            Debug.Log("Take damage : " + damage);

            
        }
    }
}
