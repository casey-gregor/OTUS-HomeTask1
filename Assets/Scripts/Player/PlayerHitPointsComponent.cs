using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerHitPointsComponent
    {
        public event Action<GameObject> hpEmptyEvent;
        
        private int hitPoints;

        private PlayerConfig playerConfig;
        private EnemyBulletSpawner enemyBulletSpawnerComponent;
        private LevelProvider levelProvider;
        private Transform player;


        public PlayerHitPointsComponent
            (
            PlayerConfig playerConfig, 
            BulletCollisionCheckComponent collisionCheckComponent,
            EnemyBulletSpawner enemyBulletSpawnerComponent,
            LevelProvider levelProvider
            )
        {
            this.playerConfig = playerConfig;
            this.hitPoints = this.playerConfig.hitPoints;
            this.enemyBulletSpawnerComponent = enemyBulletSpawnerComponent;
            this.levelProvider = levelProvider;
            this.player = this.levelProvider.player;
            collisionCheckComponent.DealDamageEvent += HandleDealDamageEvent;

        }

        private void HandleDealDamageEvent(GameObject colliderObj)
        {
            if(colliderObj == this.player.gameObject)
            {
                TakeDamage(colliderObj, this.enemyBulletSpawnerComponent.BulletConfig.damage);
            }
        }

        public bool IsAlive() {
            return this.hitPoints > 0;
        }

        public void TakeDamage(GameObject _, int damage)
        {
            this.hitPoints -= damage;
            if (!IsAlive())
            {
                this.hpEmptyEvent?.Invoke(_);
            }
        }
    }
}