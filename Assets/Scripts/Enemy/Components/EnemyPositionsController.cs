using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyPositionsController : IDisposable
    {
        private Transform[] initialSpawnPositions;
        private Transform[] initiaAttackPositions;

        private EnemyPositionsSet enemyPositionsSet;
        private EnemyHitPointsController enemyHitPointsController;

        private List<Transform> freeAttackPositions;
        private List<Transform> freeSpawnPositions;

        private Dictionary<Transform, GameObject> pointsDictionary = new();

        public EnemyPositionsController(EnemyPositionsSet enemyPositionsSet, EnemyHitPointsController enemyHitPointsController)
        {
            this.enemyPositionsSet = enemyPositionsSet;
            this.enemyHitPointsController = enemyHitPointsController;

            this.enemyHitPointsController.hpEmptyEvent += FreeUpTransform;

            this.initialSpawnPositions = this.enemyPositionsSet.spawnPositions;
            this.initiaAttackPositions = this.enemyPositionsSet.attackPositions;
            
            this.freeAttackPositions = this.initiaAttackPositions.ToList();
            this.freeSpawnPositions = this.initialSpawnPositions.ToList();
        }

        private void FreeUpTransform(GameObject enemy)
        {
            if (pointsDictionary.ContainsValue(enemy))
            {
                Transform key = pointsDictionary.FirstOrDefault(kvp => kvp.Value == enemy).Key;
                if (key != null)
                {
                    pointsDictionary.Remove(key);
                    freeAttackPositions.Add(key);
                }
            }
        }

        public Transform RandomSpawnPosition()
        {
            var index = Random.Range(0, freeSpawnPositions.Count);
            return freeSpawnPositions[index];
        }

        public Transform RandomAttackPosition(GameObject enemy)
        {
            if (freeAttackPositions.Count > 0)
            {
                var index = Random.Range(0, freeAttackPositions.Count);
                Transform attackPosition = freeAttackPositions[index];
                freeAttackPositions.RemoveAt(index);
                pointsDictionary.Add(attackPosition, enemy);
                return attackPosition;
            }
            return null;
        }

        public void Dispose()
        {
            this.enemyHitPointsController.hpEmptyEvent -= FreeUpTransform;
        }
    }
}