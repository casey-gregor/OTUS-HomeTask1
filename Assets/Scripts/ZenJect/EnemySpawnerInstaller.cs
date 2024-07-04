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
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
            Container.Bind<EnemyCheckDestinationAgent>().AsSingle().NonLazy();
            Container.Bind<EnemyMoveAgent>().AsSingle().NonLazy();
            Container.Bind<EnemyAttackAgent>().AsSingle().NonLazy();
            //Container.Bind<EnemyHitPointsAgent>().AsSingle().WithArguments(3).NonLazy();
            Container.BindInterfacesAndSelfTo<TimerService>().AsSingle();
            Container.Bind<EnemyHitPointsComponent>().AsSingle().NonLazy();
        }
    }

}
