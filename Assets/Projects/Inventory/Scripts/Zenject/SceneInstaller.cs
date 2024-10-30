using Zenject;

namespace Inventory.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Entity>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EventNotifier>().AsSingle().NonLazy();
            Container.Bind<InventoryManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ComponentsObserver>().AsSingle().NonLazy();
        }
    }
}