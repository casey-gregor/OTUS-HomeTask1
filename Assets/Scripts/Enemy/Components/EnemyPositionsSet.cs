using UnityEngine;

namespace ShootEmUp
{
    public class EnemyPositionsSet
    {
        public Transform[] spawnPositions { get; private set; }
        public Transform[] attackPositions { get; private set; }

        public EnemyPositionsSet(Transform[] spawnPositions, Transform[] attackPositions)
        {
            this.spawnPositions = spawnPositions;
            this.attackPositions = attackPositions;
        }
    }
}
