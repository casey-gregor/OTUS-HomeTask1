using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "PlayerConfigInstaller", 
        menuName = "ConfigInstallers/New PlayerConfigInstaller"
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
