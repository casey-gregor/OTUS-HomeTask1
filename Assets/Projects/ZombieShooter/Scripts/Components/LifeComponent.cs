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
        [SerializeField] private bool isDead;

        public void Construct()
        {
            TakeDamageEvent.Subscribe(TakeDamage);

            _hitPoints.Subscribe((hp) =>
            {
                if (hp <= 0)
                {
                    isDead = true;
                }
            });
        }
        public bool IsAlive()
        {
            return !isDead;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            _hitPoints.Value -= damage;
            Debug.Log("Take damage : " + damage);

            
        }
    }
}
