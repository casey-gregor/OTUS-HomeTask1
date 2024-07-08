using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "BulletConfigHolderInstaller", 
        menuName = "SOInstallers/New BulletConfigHolderInstaller"
        )]
    public class BulletConfigHolderInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BulletConfigsHolder bulletConfigsHolder;
        public override void InstallBindings()
        {
            Container.Bind<BulletConfigsHolder>().FromInstance(bulletConfigsHolder);
        }
    }
}
