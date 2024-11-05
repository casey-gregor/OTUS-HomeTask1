using System.Collections.Generic;

namespace RealTime
{
    public interface IChestSaveLoader
    {
        public ChestCollection LoadChests();
        public void SaveChests(List<Chest> chests);
    }
}