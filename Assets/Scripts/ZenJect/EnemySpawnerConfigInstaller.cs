using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName ="EnemySpawnerConfigInstaller", 
        menuName = "ConfigInstallers/New EnemySpawnerConfigInstaller"
        )]
    public class EnemySpawnerConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private EnemySpawnerConfig spawnerConfig;

        public override void InstallBindings()
        {
            Container.Bind<EnemySpawnerConfig>().FromInstance(spawnerConfig);
        }
    }
}
