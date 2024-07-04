using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponentMono : MonoBehaviour
    {
        public int hitPoints;

        private int currentHitPoints;

        public event Action<GameObject> hpEmpty;

        private void OnEnable()
        {
            currentHitPoints = hitPoints;
        }
        public bool IsAlive() {
            return this.currentHitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this.currentHitPoints -= damage;
            if (this.currentHitPoints <= 0)
            {
                this.hpEmpty?.Invoke(this.gameObject);
            }
        }
    }
}