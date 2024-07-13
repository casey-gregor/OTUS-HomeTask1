using Zenject;

namespace ShootEmUp
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawnerComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyInitializeComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyCheckDestinationComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyMoveComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyAttackComponent>().AsSingle().NonLazy();
            Container.Bind<TimerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHitPointsComponent>().AsSingle();
        }
    }

}
