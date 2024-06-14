using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int hitPoints;
        public event Action<GameObject> hpEmpty;

        public bool IsAlive() {
            return this.hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
            if (this.hitPoints <= 0)
            {
                this.hpEmpty?.Invoke(this.gameObject);
            }
        }
    }
}