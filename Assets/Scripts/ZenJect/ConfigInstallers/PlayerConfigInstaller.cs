using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    [CreateAssetMenu(
        fileName = "PlayerConfigInstaller", 
        menuName = "SOInstallers/New PlayerSOInstaller"
        )]
    public class PlayerConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(playerConfig);
        }
    }
}
