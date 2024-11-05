using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RealTime
{
    public sealed class ChestManager : MonoBehaviour
    {
        public Transform chestContainer;
        public List<ChestConfig> chestConfigs;
        
        private ChestSpawner _chestSpawner;
        private ChestActivator _chestActivator;
        private SessionLogger _sessionLogger;
        
        private IChestSaveLoader _chestSaveLoader;

        [Inject]
        public void Construct(
            ChestSpawner chestSpawner, 
            ChestActivator chestActivator,
            ChestSaveLoader chestSaveLoader,
            SessionLogger sessionLogger)
        {
            _chestSpawner = chestSpawner;
            _chestActivator = chestActivator;
            _chestSaveLoader = chestSaveLoader;
            _sessionLogger = sessionLogger;
            
        }

        private void Awake()
        {
            ChestCollection chestCollection = _chestSaveLoader.LoadChests();
            if (chestCollection != null && chestCollection.chests.Count > 0 && chestConfigs.Count > 0)
            {
                _chestSpawner.SpawnSavedChests(chestConfigs, chestCollection, chestContainer);
            }
        }
        
        [Button, EnableIf(nameof(IsServerDataAvailable))]
        public void SpawnChest(ChestConfig config)
        {
            Chest chest = _chestSpawner.SpawnChest(config, chestContainer);
            _chestActivator.ActivateChest(config.minutesBeforeOpen, chest);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }
        
        private bool IsServerDataAvailable()
        {
            return _sessionLogger != null && _sessionLogger.GotServerData;
        }

        [Button]
        public void SaveChests()
        {
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }
        
    }
}