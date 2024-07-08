using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletSpawnerConfigHolder",
        menuName = "Configs/New BulletSpanwerConfigHolder"
    )]
    public sealed class BulletSpawnerConfigHolder : ScriptableObject
    {
        public EnemyBulletSpawnerConfig forEnemy;
        public PlayerBulletSpawnerConfig forPlayer;
    }
}