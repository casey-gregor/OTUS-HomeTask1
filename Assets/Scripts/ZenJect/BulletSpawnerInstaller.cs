using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSpawnerInstaller : MonoInstaller
{
    [SerializeField] private Transform parent;
    public override void InstallBindings()
    {
        Container.Bind<Transform>().FromInstance(parent).AsSingle().NonLazy();
        Container.Bind<BulletSpawnerComponent>().AsSingle().NonLazy();
        Container.Bind<LevelBoundsCheckComponent>().AsSingle().NonLazy();
        Container.Bind<BulletInitializeComponent>().AsSingle().NonLazy();
        Container.Bind<BulletMoveComponent>().AsSingle().NonLazy();
        Container.Bind<WeaponComponent>().AsSingle().NonLazy();
        
    }
}
