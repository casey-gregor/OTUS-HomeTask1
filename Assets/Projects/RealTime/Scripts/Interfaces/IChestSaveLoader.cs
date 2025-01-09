using System.Collections.Generic;

namespace RealTime
{
    public interface IChestSaveLoader
    {
        public ChestCollection LoadChests();
        public void SaveChests(IReadOnlyList<ChestPresenter> chestsToSave);
    }
}