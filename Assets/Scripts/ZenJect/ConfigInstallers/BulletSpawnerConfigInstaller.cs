using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "BulletSpawnerConfigInstaller", 
        menuName = "SOInstallers/New BulletSpawnerSOInstaller"
        )]
    public class BulletSpawnerConfigInstaller : ScriptableObjectInstaller
    {
        //[SerializeField] private BulletSpawnerConfigHolder bulletSpawnerConfig;
        [SerializeField] private BulletSpawnerConfig config;
        public override void InstallBindings()
        {
            //Container.Bind<BulletSpawnerConfigHolder>().FromInstance(bulletSpawnerConfig).AsSingle();
            Container.Bind<BulletSpawnerConfig>().FromInstance(config).AsSingle();
        }
    }
}
