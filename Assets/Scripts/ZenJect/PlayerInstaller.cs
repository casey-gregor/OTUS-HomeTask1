using ShootEmUp;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
        //Container.Bind<WeaponComponentMono>().FromComponentOnRoot().AsSingle();
        //Container.Bind<CollisionCheckComponentMono>().FromComponentOnRoot().AsSingle();
        Container.Bind<PlayerHitPointsComponent>().AsSingle().NonLazy();
        Container.Bind<PlayerHealthObserverComponent>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CharacterShootAgent>().AsSingle();
        Container.BindInterfacesAndSelfTo<CharacterMoveAgent>().AsSingle();
    }
}
