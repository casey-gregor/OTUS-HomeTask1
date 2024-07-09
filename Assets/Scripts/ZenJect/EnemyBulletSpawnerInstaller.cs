using ShootEmUp;
using UnityEngine;
using Zenject;

public class EnemyBulletSpawnerInstaller : MonoInstaller
{
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private BulletConfig bulletConfig;
    [SerializeField] private BulletSpawnerConfig bulletSpawnerConfig;

    public override void InstallBindings()
    {
        Container.Bind<EnemyBulletSpawnerComponent>().AsSingle().WithArguments(bulletConfig, bulletSpawnerConfig, bulletContainer);
    }
}
