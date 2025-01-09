using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestSpawner
    {
        public event Action SpawnedSavedChests;
        public IReadOnlyList<ChestPresenter> SpawnedChests => _spawnedChests;
        private readonly List<ChestPresenter> _spawnedChests = new();
        private readonly Dictionary<string, ChestConfigData> _chestConfigDataDict;
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
                    ChestPresenter chestPresenter = SpawnChest(chestConfigData.ChestPrefab, chestContainer);
                    _savedChestsInitializer.InitializeSavedChest(
                        chestPresenter,
                        chestConfigData.ChestId,
                        chestConfigData.Rewards,
                        chestConfigData.InitialTimer,
                        chestSaveData.TimeToOpen,
                        chestSaveData.IsUnlocked);
                }
            }
            SpawnedSavedChests?.Invoke();
            
        }

        public ChestPresenter SpawnChest(GameObject prefab, Transform chestContainer)
        {
            GameObject chest = GameObject.Instantiate(prefab, chestContainer);
            ChestView chestView = chest.GetComponentInChildren<ChestView>();
            ChestModel chestModel = new ChestModel();
            ChestPresenter chestPresenter = new ChestPresenter(chestModel, chestView);
            _spawnedChests.Add(chestPresenter);
            
            return chestPresenter;
        }

        public void RemoveChest(ChestPresenter chestPresenter)
        {
            _spawnedChests.Remove(chestPresenter);
        }
    }
    
}