using System.Collections.Generic;
using RealTime.Rewards;
using UnityEngine;
using Zenject;

namespace RealTime.Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private List<ChestConfig> chestConfigs;
        [SerializeField] private RewardsView rewardsPopupPrefab;
        
        public override void InstallBindings()
        {
            
            Container.Bind<IReadOnlyList<ChestConfigData>>().FromInstance(CreateChestConfigDataList(chestConfigs)).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPrefsSessionSaveLoader>().AsSingle().NonLazy();
            Container.Bind<SessionDataManager>().AsSingle().NonLazy();
            Container.Bind<LogView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ServerConnectView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SessionController>().AsSingle().NonLazy();
            Container.Bind<SessionPresenter>().AsSingle().NonLazy();
            Container.Bind<ServerConnectPresenter>().AsSingle().NonLazy();
            // Container.BindInterfacesAndSelfTo<WorldTimeAPIRetriever>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TestServerTimeRetriever>().AsSingle().NonLazy();
            Container.Bind<LogManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Entity>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SaveLoadToJson>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestSaveLoader>().AsSingle().NonLazy();
            Container.Bind<SavedChestsInitializer>().AsSingle().NonLazy();
            Container.Bind<ChestSpawner>().AsSingle().NonLazy();
            Container.Bind<ChestActivator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestDestroyer>().AsSingle().NonLazy();
            Container.Bind<ChestDataFactory>().AsSingle().NonLazy();
            Container.Bind<ChestLocker>().AsSingle().NonLazy();
            Container.Bind<ChestButtonTracker>().AsSingle().NonLazy();
            Container.Bind<ApplyChestReward>().AsSingle().NonLazy();
            Container.Bind<ChestManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ServerEventDispatcher>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestEventsDispatcher>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChestTimerCalculator>().AsSingle().NonLazy();
            Container.Bind<RewardsView>().FromInstance(rewardsPopupPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RewardsMover>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RewardsPresenter>().AsSingle().NonLazy();
        }
        
        private IReadOnlyList<ChestConfigData> CreateChestConfigDataList(List<ChestConfig> chestConfigs)
        {
            if (chestConfigs.Count == 0)
            {
                Debug.Log("No chest config found");
            }
            List<ChestConfigData> chestConfigDataList = new List<ChestConfigData>();
            foreach (var config in chestConfigs)
            {
                chestConfigDataList.Add(config.GetChestData());
            }
            return chestConfigDataList;
        }
    }
    
    
}