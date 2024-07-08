using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerHitPointsComponent
    {
        private int hitPoints;

        private PlayerConfig playerConfig;
        private EnemyBulletSpawnerComponent enemyBulletSpawnerComponent;
        private Transform player;

        public event Action<GameObject> hpEmptyEvent;

        public PlayerHitPointsComponent(
            PlayerConfig playerConfig, 
            BulletCollisionCheckComponent collisionCheckComponent,
            EnemyBulletSpawnerComponent enemyBulletSpawnerComponent,
            [Inject(Id = BindingIds.playerId)] Transform player
            )
        {
            this.playerConfig = playerConfig;
            hitPoints = this.playerConfig.hitPoints;
            this.enemyBulletSpawnerComponent = enemyBulletSpawnerComponent;
            this.player = player;
            collisionCheckComponent.DealDamageEvent += HandleDealDamageEvent;
            //Debug.Log("hitpoints component");
        }

        private void HandleDealDamageEvent(GameObject colliderObj)
        {
            if(colliderObj == player.gameObject)
            {
                TakeDamage(colliderObj, this.enemyBulletSpawnerComponent.BulletConfig.damage);
            }
        }

        public bool IsAlive() {
            return hitPoints > 0;
        }

        public void TakeDamage(GameObject _, int damage)
        {
            hitPoints -= damage;
            if (!IsAlive())
            {
                this.hpEmptyEvent?.Invoke(_);
            }
        }
    }
}