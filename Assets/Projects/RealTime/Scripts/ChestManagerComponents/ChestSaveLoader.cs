using System.Collections.Generic;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestSaveLoader : IChestSaveLoader
    {
        private readonly SaveLoadToJson _saveLoadJson;
        private readonly ChestDataFactory _chestDataFactory;

        public ChestSaveLoader(
            SaveLoadToJson saveLoadJson, 
            ChestDataFactory chestDataFactory)
        {
            _saveLoadJson = saveLoadJson;
            _chestDataFactory = chestDataFactory;
        }

        public ChestCollection LoadChests()
        {
            return _saveLoadJson.LoadChests();
        }

        public void SaveChests(List<ChestModel> chestsToSave)
        {
            ChestCollection chestCollection = new ChestCollection();
            foreach (ChestModel chest in chestsToSave)
            {
                Debug.Log("in chest save loader. chest default timer : " + chest.InitialTimer);
                ChestSaveData chestSaveData = _chestDataFactory.CreateChestData(chest);
                chestCollection.chests.Add(chestSaveData);
            }
            
            _saveLoadJson.SaveChests(chestCollection);
        }
    }
}