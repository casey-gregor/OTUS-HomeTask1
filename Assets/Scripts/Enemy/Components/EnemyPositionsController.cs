using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionsController
    {
        private Transform[] spawnPositions;
        private Transform[] attackPositions;

        private EnemyPositionsSet enemyPositionsSet;

        public EnemyPositionsController(EnemyPositionsSet enemyPositionsSet)
        {
            this.enemyPositionsSet = enemyPositionsSet;

            this.spawnPositions = this.enemyPositionsSet.spawnPositions;
            this.attackPositions = this.enemyPositionsSet.attackPositions;
        }
        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}