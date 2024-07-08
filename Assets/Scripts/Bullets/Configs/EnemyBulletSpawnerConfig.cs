using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemyBulletSpawnerConfig",
        menuName = "Configs/New EnemyBulletSpanwerConfig"
    )]
    public sealed class EnemyBulletSpawnerConfig : ScriptableObject
    {
        public int initialCount = 50;
        public GameObject prefab;
    }
}