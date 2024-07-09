using UnityEngine;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemySpawnerConfig", 
        menuName ="Configs/New EnemySpawnerConfig"
        )]
    public class EnemySpawnerConfig : ScriptableObject
    {
        public int initialEnemiesCount = 7;
        public float spawnEnemiesEveryNumOfSeconds = 5;
        public GameObject enemyPrefab;
    }

}
