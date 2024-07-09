using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Configs/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        public bool isPlayer;
        public PhysicsLayer physicsLayer;
        public Color color;
        public int damage;
        public float speed;
    }
}