
namespace RealTime
{
    public sealed class ChestDataFactory
    {
        public ChestSaveData CreateChestData(ChestModel chestModel)
        {
            string timeToOpen = TextFormatter.DateTimeToString(chestModel.TimeToOpen);
            return new ChestSaveData(
                chestModel.ChestId, 
                timeToOpen,
                (int)chestModel.InitialTimer.TotalMinutes,
                chestModel.IsUnlocked);;
        }
    }
}