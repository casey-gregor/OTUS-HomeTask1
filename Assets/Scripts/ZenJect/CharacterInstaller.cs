using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
        Container.Bind<WeaponComponentMono>().FromComponentOnRoot().AsSingle();
        Container.Bind<CollisionCheckComponentMono>().FromComponentOnRoot().AsSingle();
        //Container.Bind<EnemyHitPointsComponent>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CharacterShootAgent>().AsSingle();
        Container.BindInterfacesAndSelfTo<CharacterMoveAgent>().AsSingle();
    }
}
