using Zenject;

namespace RealTime.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerPrefsSessionSaveLoader>().AsSingle().NonLazy();
            Container.Bind<SessionDataManager>().AsSingle().NonLazy();
            Container.Bind<LogView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ServerConnectView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SessionLogger>().AsSingle().NonLazy();
            Container.Bind<SessionPresenter>().AsSingle().NonLazy();
            Container.Bind<ServerConnectPresenter>().AsSingle().NonLazy();
            Container.Bind<ServerTimeGetter>().AsSingle().NonLazy();
            Container.Bind<ServerTimeProcessor>().AsSingle().NonLazy();
            Container.Bind<UtcTimeCalculator>().AsSingle().NonLazy();
            Container.Bind<LogManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Entity>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SaveLoadToJson>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestSaveLoader>().AsSingle().NonLazy();
            Container.Bind<ChestInitializer>().AsSingle().NonLazy();
            Container.Bind<ChestSpawner>().AsSingle().NonLazy();
            Container.Bind<ChestActivator>().AsSingle().NonLazy();
            Container.Bind<ChestDataFactory>().AsSingle().NonLazy();
            Container.Bind<ChestLockChecker>().AsSingle().NonLazy();
            Container.Bind<ChestLocker>().AsSingle().NonLazy();
            Container.Bind<ChestButtonTracker>().AsSingle().NonLazy();
            Container.Bind<ApplyChestReward>().AsSingle().NonLazy();
            Container.Bind<ChestManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EventDispatcher>().AsSingle().NonLazy();
        }
    }
}