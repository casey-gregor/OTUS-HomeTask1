using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyHitPointsComponent : IDisposable
    {
        public event Action<GameObject> hpEmptyEvent;
        
        private EnemyConfig enemyConfig;
        private BulletCollisionCheckComponent collisionCheckComponent;
        private PlayerBulletSpawner playerBulletSpawnerComponent;

        private EnemySpawnerComponent enemySpawnerComponent;

        private Dictionary<GameObject, int> hitPointsDict;


        public EnemyHitPointsComponent(
            EnemyConfig enemyConfig, 
            BulletCollisionCheckComponent collisionCheckComponent, 
            PlayerBulletSpawner playerBulletSpawnerComponent
            )
        {
            this.enemyConfig = enemyConfig;
            this.collisionCheckComponent = collisionCheckComponent;
            this.playerBulletSpawnerComponent = playerBulletSpawnerComponent;

            this.collisionCheckComponent.DealDamageEvent += HandleDealDamageEvent;
            
            this.hitPointsDict = new Dictionary<GameObject, int>();
        }

        public void SetSpawnerAndSubscribe(EnemySpawnerComponent spawner)
        {
            this.enemySpawnerComponent = spawner;
            this.enemySpawnerComponent.enemySpawnedEvent += HandleEnemySpawnEvent;
        }

        private void HandleDealDamageEvent(GameObject damagedObject)
        {
            if(this.hitPointsDict.ContainsKey(damagedObject))
            {
                TakeDamage(damagedObject, this.playerBulletSpawnerComponent.BulletConfig.damage);
            }
        }

        private void HandleEnemySpawnEvent(GameObject enemyObject)
        {
            if(this.hitPointsDict.ContainsKey(enemyObject))
            {
                this.hitPointsDict[enemyObject] = this.enemyConfig.hitPoints;
            }
            else
            {
                this.hitPointsDict.Add(enemyObject, this.enemyConfig.hitPoints);
            }
        }
       
        private bool IsAlive(GameObject enemyObject) 
        {
            return this.hitPointsDict[enemyObject] > 0;
        }

        private void TakeDamage(GameObject obj, int damage)
        {
            this.hitPointsDict[obj] -= damage;
            if (!IsAlive(obj))
            {
                this.hpEmptyEvent?.Invoke(obj);
            }
        }

        public void Dispose()
        {
            this.enemySpawnerComponent.enemySpawnedEvent -= HandleEnemySpawnEvent;
        }
    }
}