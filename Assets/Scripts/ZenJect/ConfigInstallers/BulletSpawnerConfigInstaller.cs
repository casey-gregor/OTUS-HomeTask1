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
        [SerializeField] private BulletSpawnerConfig bulletSpawnerConfig;
        public override void InstallBindings()
        {
            Container.Bind<BulletSpawnerConfig>().FromInstance(bulletSpawnerConfig);
        }
    }
}
