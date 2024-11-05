using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestSpawner
    {
        public List<Chest> SpawnedChests = new();

        private readonly ChestInitializer _chestInitializer;

        public ChestSpawner(ChestInitializer chestInitializer)
        {
            _chestInitializer = chestInitializer;
        }
        public void SpawnSavedChests(
            List<ChestConfig> chestConfigs, 
            ChestCollection chestCollection,
            Transform chestContainer)
        {
            foreach (ChestData chestData in chestCollection.chests)
            {
                ChestConfig config = chestConfigs.Find(chestConfig => chestConfig != null &&
                    chestConfig.chestId == chestData.ChestId);
                if (config != null)
                {
                    Chest chestComponent = SpawnChest(config, chestContainer);
                    _chestInitializer.InitializeChest(chestComponent, chestData);
                }
            }
        }

        public Chest SpawnChest(ChestConfig config, Transform chestContainer)
        {
            GameObject chest = GameObject.Instantiate(config.chestPrefab, chestContainer);
            chest.name = config.chestId;
            Chest chestComponent = chest.GetComponent<Chest>();
            chestComponent.SetId(config.chestId);
            chestComponent.SetBonuses(config.bonuses);
            chestComponent.SetTimerMinutes(TimeSpan.FromMinutes(config.minutesBeforeOpen));
            SpawnedChests.Add(chestComponent);

            return chestComponent;
        }
    }
}