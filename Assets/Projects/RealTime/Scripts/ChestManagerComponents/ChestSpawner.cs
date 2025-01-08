using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestSpawner
    {
        public event Action SpawnedSavedChests;
        public List<ChestModel> SpawnedChests = new();
        private readonly Dictionary<string, ChestConfigData> _chestConfigDataDict = new();
        private readonly SavedChestsInitializer _savedChestsInitializer;

        public ChestSpawner(
            IReadOnlyList<ChestConfigData> chestConfigsData,
            SavedChestsInitializer savedChestsInitializer)
        {
            _chestConfigDataDict = chestConfigsData.ToDictionary(configData => configData.ChestId);
            _savedChestsInitializer = savedChestsInitializer;
        }
        public void SpawnSavedChests(
            ChestCollection chestCollection,
            Transform chestContainer)
        {
            foreach (ChestSaveData chestSaveData in chestCollection.chests)
            {
                if (_chestConfigDataDict.TryGetValue(chestSaveData.ChestId, out ChestConfigData chestConfigData))
                {
                    ChestModel chestModelComponent = SpawnChest(chestConfigData.ChestPrefab, chestContainer);
                    _savedChestsInitializer.InitializeSavedChest(
                        chestModelComponent,
                        chestConfigData.ChestId,
                        chestConfigData.Rewards,
                        chestConfigData.InitialTimer,
                        chestSaveData.TimeToOpen,
                        chestSaveData.IsUnlocked);
                }
            }
            SpawnedSavedChests?.Invoke();
            
        }

        public ChestModel SpawnChest(GameObject prefab, Transform chestContainer)
        {
            GameObject chest = GameObject.Instantiate(prefab, chestContainer);
            ChestView chestView = chest.GetComponentInChildren<ChestView>();
            ChestPresenter chestPresenter = new ChestPresenter(chestView);
            ChestModel chestModel = new ChestModel(chestPresenter);
            SpawnedChests.Add(chestModel);
            
            return chestModel;
        }
    }
    
}