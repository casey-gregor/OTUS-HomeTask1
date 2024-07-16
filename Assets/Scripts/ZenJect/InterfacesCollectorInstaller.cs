using Zenject;

namespace ShootEmUp
{
    public class InterfacesCollectorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        
            Container.Bind<InterfaceCollector>().AsSingle().NonLazy();
        }
    }

}
