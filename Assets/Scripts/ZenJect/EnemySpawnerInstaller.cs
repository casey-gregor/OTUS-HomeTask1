using Zenject;

namespace ShootEmUp
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawnerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyInitializeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyCheckDestinationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyAttackController>().AsSingle().NonLazy();
            Container.Bind<Timer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHitPointsController>().AsSingle();
        }
    }

}
