using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "PlayerBulletSpawnerConfig",
        menuName = "Configs/New PlayerBulletSpanwerConfig"
    )]
    public sealed class PlayerBulletSpawnerConfig : ScriptableObject
    {
        public int initialCount = 50;
        public GameObject prefab;

    }
}