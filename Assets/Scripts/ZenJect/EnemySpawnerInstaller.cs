using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnerComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyCheckDestinationComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyMoveComponent>().AsSingle().NonLazy();
            Container.Bind<EnemyAttackComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TimerService>().AsSingle();
            Container.Bind<EnemyHitPointsComponent>().AsSingle();
        }
    }

}
