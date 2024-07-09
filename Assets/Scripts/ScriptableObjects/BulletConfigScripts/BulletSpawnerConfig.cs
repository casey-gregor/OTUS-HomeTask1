using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletSpawnerConfig",
        menuName = "Configs/New BulletSpawnerConfig"
    )]
    public sealed class BulletSpawnerConfig : ScriptableObject
    {
        public int initialCount = 20;
        public GameObject prefab;
    }
}