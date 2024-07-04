using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyHitPointsComponent
    {
        public int hitPoints;

        private EnemyConfig enemyConfig;

        private Dictionary<GameObject, int> hitPointsDict;

        public event Action<GameObject> hpEmpty;

        public EnemyHitPointsComponent(EnemyConfig enemyConfig)
        {
            hitPointsDict = new Dictionary<GameObject, int>();
            this.enemyConfig = enemyConfig;
        }

        public void SubscribeToSpawner(EnemySpawner spawner)
        {
            spawner.enemySpawnedEvent += HandleEnemySpawnEvent;
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

        public void TakeDamage(GameObject obj, int damage)
        {
            hitPointsDict[obj] -= damage;
            if (!IsAlive(obj))
            {
                this.hpEmpty?.Invoke(obj);
            }
        }
    }
}