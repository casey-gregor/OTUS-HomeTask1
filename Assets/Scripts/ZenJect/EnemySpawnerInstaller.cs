using Zenject;

namespace ShootEmUp
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawnerComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyInitializeComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyCheckDestinationComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyMoveComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyAttackComponent>().AsSingle().NonLazy();
            Container.Bind<TimerService>().AsSingle();
            Container.Bind<EnemyHitPointsComponent>().AsSingle();
        }
    }

}
