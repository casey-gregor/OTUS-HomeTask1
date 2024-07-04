using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletSpawnerConfig",
        menuName = "Configs/New BulletSpanwerConfig"
    )]
    public sealed class BulletSpawnerConfig : ScriptableObject
    {
        public int initialCount = 50;
        public GameObject prefab;
        //public Transform container;
    }
}