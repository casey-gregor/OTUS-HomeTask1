using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyHitPointsComponent
    {
        public int hitPoints;

        private EnemyConfig enemyConfig;
        private BulletCollisionCheckComponent collisionCheckComponent;
        private PlayerBulletSpawnerComponent playerBulletSpawnerComponent;

        private Dictionary<GameObject, int> hitPointsDict;

        public event Action<GameObject> hpEmptyEvent;

        public EnemyHitPointsComponent
            (
            EnemyConfig enemyConfig, 
            BulletCollisionCheckComponent collisionCheckComponent, 
            PlayerBulletSpawnerComponent playerBulletSpawnerComponent
            )
        {
            hitPointsDict = new Dictionary<GameObject, int>();
            this.enemyConfig = enemyConfig;
            this.collisionCheckComponent = collisionCheckComponent;
            this.playerBulletSpawnerComponent = playerBulletSpawnerComponent;

            this.collisionCheckComponent.DealDamageEvent += HandleDealDamageEvent;
        }

        public void SubscribeToSpawner(EnemySpawnerComponent spawner)
        {
            spawner.enemySpawnedEvent += HandleEnemySpawnEvent;
        }

        private void HandleDealDamageEvent(GameObject obj)
        {
            if(hitPointsDict.ContainsKey(obj))
            {
                TakeDamage(obj, this.playerBulletSpawnerComponent.BulletConfig.damage);
            }
        }

        private void HandleEnemySpawnEvent(GameObject obj, Transform transform)
        {
            if(hitPointsDict.ContainsKey(obj))
            {
                hitPointsDict[obj] = enemyConfig.hitPoints;
            }
            else
            {
                hitPointsDict.Add(obj, enemyConfig.hitPoints);
            }
        }
       
        public bool IsAlive(GameObject obj) {
            return hitPointsDict[obj] > 0;
        }

        private void TakeDamage(GameObject obj, int damage)
        {
            hitPointsDict[obj] -= damage;
            if (!IsAlive(obj))
            {
                this.hpEmptyEvent?.Invoke(obj);
            }
        }
    }
}