using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions
    {
        private Transform[] spawnPositions;
        private Transform[] attackPositions;

        public EnemyPositions(Transform[] spawnPositions, Transform[] attackPositions)
        {
            this.spawnPositions = spawnPositions;
            this.attackPositions = attackPositions;
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