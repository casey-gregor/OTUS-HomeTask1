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

        public void SaveChests(IReadOnlyList<ChestPresenter> chestsToSave)
        {
            ChestCollection chestCollection = new ChestCollection();
            foreach (ChestPresenter chestPresenter in chestsToSave)
            {
                ChestSaveData chestSaveData = _chestDataFactory.CreateChestData(chestPresenter);
                chestCollection.chests.Add(chestSaveData);
            }
            
            _saveLoadJson.SaveChests(chestCollection);
        }
    }
}