using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RealTime
{
    public sealed class ChestManager : MonoBehaviour
    {
        public event Action OnChestSpawned;
        
        public Transform chestContainer;
        // public List<ChestConfig> chestConfigs;
        
        private IReadOnlyList<ChestConfigData> _chestConfigsData;
        private ChestSpawner _chestSpawner;
        private ChestActivator _chestActivator;
        private ChestDestroyer _chestDestroyer;
        private SessionController _sessionController;
        
        private IChestSaveLoader _chestSaveLoader;

        [Inject]
        public void Construct(
            IReadOnlyList<ChestConfigData> chestConfigsData,
            ChestSpawner chestSpawner, 
            ChestActivator chestActivator,
            ChestDestroyer chestDestroyer,
            ChestSaveLoader chestSaveLoader,
            SessionController sessionController)
        {
            _chestConfigsData = chestConfigsData;
            _chestSpawner = chestSpawner;
            _chestActivator = chestActivator;
            _chestDestroyer = chestDestroyer;
            _chestSaveLoader = chestSaveLoader;
            _sessionController = sessionController;
            
        }

        private void Awake()
        {
            ChestCollection chestCollection = _chestSaveLoader.LoadChests();
            if (chestCollection != null && chestCollection.chests.Count > 0 && _chestConfigsData.Count > 0)
            {
                _chestSpawner.SpawnSavedChests(chestCollection, chestContainer);
            }
        }
        
        [Button, EnableIf(nameof(IsServerDataAvailable))]
        public void SpawnChest(ChestConfig config)
        {
            ChestConfigData configData = config.GetChestData();
            ChestModel chestModel = _chestSpawner.SpawnChest(configData.ChestPrefab, chestContainer);
            _chestActivator.ActivateChest(configData.InitialTimer, chestModel);
            _chestActivator.SetChestData(chestModel, configData.ChestId, configData.Rewards);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
            OnChestSpawned?.Invoke();
        }

        [Button]
        public void DestroyChest(ChestConfig config)
        {
            _chestDestroyer.DestroyChest(config.GetChestData());
        }
        
        [Button]
        public void SaveChests()
        {
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }
        
        private bool IsServerDataAvailable()
        {
            return _sessionController != null && _sessionController.GotServerTime;
        }

        
        
    }
}