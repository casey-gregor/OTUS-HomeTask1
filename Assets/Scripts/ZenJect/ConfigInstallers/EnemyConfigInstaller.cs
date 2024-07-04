using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "EnemyConfigInstaller", 
        menuName = "SOInstallers/New EnemySOInstaller"
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
