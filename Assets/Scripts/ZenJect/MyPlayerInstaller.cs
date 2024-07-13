using ShootEmUp;
using UnityEngine;
using Zenject;

public class MyPlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
        Container.Bind<PlayerHitPointsComponent>().AsSingle().NonLazy();
        Container.Bind<PlayerHealthObserverComponent>().AsSingle().NonLazy();
        Container.Bind<PlayerShootComponent>().AsSingle().NonLazy();
        Container.Bind<PlayerMoveComponent>().AsSingle().NonLazy();
    }
}