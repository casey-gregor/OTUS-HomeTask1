using ShootEmUp;
using UnityEngine;
using Zenject;

public class PlayerBulletSpawnerInstaller : MonoInstaller
{
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private BulletConfig bulletConfig;
    [SerializeField] private BulletSpawnerConfig bulletSpawnerConfig;

    public override void InstallBindings()
    {
        //Container.Bind<Transform>().FromInstance(bulletContainer).AsSingle();
        //Container.Bind<BulletConfig>().FromInstance(bulletConfig).AsSingle();
        //Container.Bind<BulletSpawnerConfig>().FromInstance(bulletSpawnerConfig).AsSingle();
        Container.Bind<PlayerBulletSpawnerComponent>().AsSingle().WithArguments(bulletConfig, bulletSpawnerConfig, bulletContainer);
    }
}
