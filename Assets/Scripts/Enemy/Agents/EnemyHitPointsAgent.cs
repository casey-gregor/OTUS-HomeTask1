using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyHitPointsAgent
    {

        private int currentHitPoints;

        public event Action hpEmpty;

        private CollisionCheckAgent collisionCheckAgent;
        private BulletConfig bulletConfig;

        public EnemyHitPointsAgent(int hitPoints, CollisionCheckAgent collisionCheckAgent, BulletConfig bulletConfig)
        {
            this.currentHitPoints = hitPoints;
            this.collisionCheckAgent = collisionCheckAgent;
            this.bulletConfig = bulletConfig;

            this.collisionCheckAgent.OnCollisionEntered += HandleOnCollisionEntered;
        }
        public bool IsAlive()
        {
            return this.currentHitPoints > 0;
        }

        private void HandleOnCollisionEntered(GameObject obj)
        {
            int damage = bulletConfig.damage;
            TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            this.currentHitPoints -= damage;
            if (this.currentHitPoints <= 0)
            {
                this.hpEmpty?.Invoke();
            }
        }
    }

}
