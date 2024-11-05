using System.Collections.Generic;

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

        public void SaveChests(List<Chest> chestsToSave)
        {
            ChestCollection chestCollection = new ChestCollection();
            foreach (Chest chest in chestsToSave)
            {
                ChestData chestData = _chestDataFactory.CreateChestData(chest);
                chestCollection.chests.Add(chestData);
            }
            
            _saveLoadJson.SaveChests(chestCollection);
        }
    }
}