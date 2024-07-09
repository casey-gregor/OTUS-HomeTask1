using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName ="LevelbackgroundConfigInstaller", 
        menuName ="ConfigInstallers/New LevelbackgroundConfigInstaller")]
    public class LevelBackgroundConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelbackgroundConfig LevelbackgroundConfig;
        public override void InstallBindings()
        {
            Container.Bind<LevelbackgroundConfig>().FromInstance(LevelbackgroundConfig);
        }
    }

}
