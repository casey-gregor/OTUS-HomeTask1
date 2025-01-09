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
        
        [Button, EnableIf(nameof(IsServerDataAvailable))]
        public void SpawnChest(ChestConfig config)
        {
            ChestConfigData configData = config.GetChestData();
            ChestPresenter chestPresenter = _chestSpawner.SpawnChest(configData.ChestPrefab, chestContainer);
            _chestActivator.ActivateChest(configData.InitialTimer, chestPresenter);
            _chestActivator.SetChestData(chestPresenter, configData.ChestId, configData.Rewards);
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
        
        private void Awake()
        {
            ChestCollection chestCollection = _chestSaveLoader.LoadChests();
            if (chestCollection != null && chestCollection.chests.Count > 0 && _chestConfigsData.Count > 0)
            {
                _chestSpawner.SpawnSavedChests(chestCollection, chestContainer);
            }
        }
        
        private bool IsServerDataAvailable()
        {
            return _sessionController != null && _sessionController.GotServerTime;
        }
    }
}