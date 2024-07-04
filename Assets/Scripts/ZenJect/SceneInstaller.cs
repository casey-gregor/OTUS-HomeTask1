using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    
    [SerializeField] private BulletSpawner playerBulletSpawner;
    [SerializeField] private Transform worldTransform;
    [SerializeField] private Transform character;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private GameObject background;
    [SerializeField] private LevelBoundsSet levelBounds;
    [SerializeField] private EnemyPositionsSet enemyPositions;

    [Serializable] public class LevelBoundsSet
    {
        public Transform left;
        public Transform right;
        public Transform top;
        public Transform bottom;
    }

    [Serializable] public class EnemyPositionsSet
    {
        public Transform[] spawnPositions;
        public Transform[] attackPositions;
    }
  
    public override void InstallBindings()
    {
        Container.Bind<ListenersStorage>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelBackground>().AsSingle().WithArguments(background).NonLazy();
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelBoundsComponent>().
            AsSingle().
            WithArguments(levelBounds.left, levelBounds.right, levelBounds.top, levelBounds.bottom).
            NonLazy();
        Container.Bind<EnemyPositions>().
            AsSingle().
            WithArguments(enemyPositions.spawnPositions, enemyPositions.attackPositions).
            NonLazy();
        
        
        
        Container.Bind<BulletSpawner>().WithId(BindingIds.playerId).FromInstance(playerBulletSpawner);

        
        Container.Bind<CollisionCheckAgent>().AsSingle();
        Container.Bind<Transform>().WithId(BindingIds.worldTransform).FromInstance(worldTransform);
        Container.Bind<Transform>().WithId(BindingIds.playerId).FromInstance(character);
        Container.Bind<Transform>().WithId(BindingIds.enemyContainer).FromInstance(enemyContainer);
    }
}
