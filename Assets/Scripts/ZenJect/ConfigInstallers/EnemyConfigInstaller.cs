using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "EnemyConfigInstaller", 
        menuName = "ConfigInstallers/New EnemyConfigInstaller"
        )]
    public class EnemyConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private EnemyConfig enemyConfig;
        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(enemyConfig);
        }
    }
}
