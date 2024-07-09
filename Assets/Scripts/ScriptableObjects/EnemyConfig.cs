using UnityEngine;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemyConfig", 
        menuName = "Configs/New EnemyConfig"
        )]
    public class EnemyConfig : ScriptableObject
    {
        public float timeBetweenShots = 3;
        public int hitPoints = 3;

    }

}
