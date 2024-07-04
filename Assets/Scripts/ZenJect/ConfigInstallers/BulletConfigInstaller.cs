using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "BulletConfigInstaller", 
        menuName = "SOInstallers/New BulletSOInstaller"
        )]
    public class BulletConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BulletConfig bulletConfig;
        public override void InstallBindings()
        {
            Container.Bind<BulletConfig>().FromInstance(bulletConfig);
        }
    }
}
