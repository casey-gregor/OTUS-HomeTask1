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

        [SerializeField] private Transform levelBoundsLeft;
        [SerializeField] private Transform levelBoundsRight;
        [SerializeField] private Transform levelBoundsTop;
        [SerializeField] private Transform levelBoundsBottom;

        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private Transform[] attackPositions;
       
  
        public override void InstallBindings()
        {
            InstallLevelProvider();
            InstallCommonComponents();
            InstallBulletRelatedComponents();
        
        }

        private void InstallLevelProvider()
        {
            //Container.Bind<Transform>().WithId(IdCollection.worldTransform).FromInstance(worldTransform);
            //Container.Bind<Transform>().WithId(IdCollection.playerId).FromInstance(character);
            //Container.Bind<Transform>().WithId(IdCollection.enemyContainer).FromInstance(enemyContainer);
            Container.Bind<LevelBackground>().AsSingle().WithArguments(background).NonLazy();
            Container.Bind<LevelBoundsSet>().AsSingle().
                WithArguments(levelBoundsLeft, levelBoundsRight, levelBoundsTop, levelBoundsBottom).
                NonLazy();
            Container.Bind<EnemyPositionsSet>().AsSingle().
                WithArguments(spawnPositions, attackPositions).
                NonLazy();
            Container.Bind<LevelProvider>().
                AsSingle().
                WithArguments(worldTransform, character, enemyContainer, background).     
                NonLazy();
        }

        private void InstallCommonComponents()
        {
   
            Container.BindInterfacesAndSelfTo<ListenersStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<EnemyPositionsComponent>().AsSingle().NonLazy();

            Container.Bind<WeaponComponent>().AsSingle();
        }

        private void InstallBulletRelatedComponents()
        {
            Container.BindInterfacesAndSelfTo<BulletInitializeComponent>().AsSingle();
            Container.Bind<BulletCollisionCheckComponent>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBoundsCheckController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletMoveComponent>().AsSingle().NonLazy();

            Container.Bind<LevelBoundsController>().AsSingle().NonLazy();
        }
    }

}
