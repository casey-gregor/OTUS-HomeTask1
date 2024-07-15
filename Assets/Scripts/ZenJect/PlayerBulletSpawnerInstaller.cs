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
        Container.Bind<PlayerBulletSpawner>().AsSingle().WithArguments(bulletConfig, bulletSpawnerConfig, bulletContainer);
    }
}
