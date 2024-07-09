using UnityEngine;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "LevelbackgroundConfig", 
        menuName = "Configs/New LevelbackgroundConfig"
        )]
    public class LevelbackgroundConfig : ScriptableObject
    {
        public float startPositionY = 19;
        public float endPositionY = -38;
        public float movingSpeedY = 5;
    }
}
