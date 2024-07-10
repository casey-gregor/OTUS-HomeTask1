using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
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
            InstallCommonComponents();
            InstallBulletRelatedComponents();
        
        }

        private void InstallCommonComponents()
        {
   
            Container.Bind<ListenersStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelBackground>().AsSingle().WithArguments(background).NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();

            Container.Bind<EnemyPositionsComponent>().
                AsSingle().
                WithArguments(enemyPositions.spawnPositions, enemyPositions.attackPositions).
                NonLazy();

            Container.Bind<Transform>().WithId(IdCollection.worldTransform).FromInstance(worldTransform);
            Container.Bind<Transform>().WithId(IdCollection.playerId).FromInstance(character);
            Container.Bind<Transform>().WithId(IdCollection.enemyContainer).FromInstance(enemyContainer);
            Container.Bind<WeaponComponent>().AsSingle();
        }

        private void InstallBulletRelatedComponents()
        {
            Container.Bind<BulletInitializeComponent>().AsSingle();
            Container.Bind<BulletCollisionCheckComponent>().AsSingle();
            Container.Bind<LevelBoundsCheckComponent>().AsSingle();
            Container.Bind<BulletObserver>().AsSingle().NonLazy();
            Container.Bind<BulletMoveComponent>().AsSingle().NonLazy();

            Container.Bind<LevelBoundsComponent>().
                AsSingle().
                WithArguments(levelBounds.left, levelBounds.right, levelBounds.top, levelBounds.bottom).
                NonLazy();
        }
    }

}
