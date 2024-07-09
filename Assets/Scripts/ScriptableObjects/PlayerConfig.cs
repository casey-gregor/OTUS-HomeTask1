using UnityEngine;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "PlayerConfig", 
        menuName = "Configs/New PlayerConfig"
        )]
    public class PlayerConfig : ScriptableObject
    {
        public float moveSpeed = 5f;
        public int hitPoints = 5;
    }
}
