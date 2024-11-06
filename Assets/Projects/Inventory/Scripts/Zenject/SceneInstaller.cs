using Zenject;

namespace Inventory.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEntity>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InventoryEventNotifier>().AsSingle().NonLazy();
            Container.Bind<InventoryManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ComponentsObserver>().AsSingle().NonLazy();
        }
    }
}