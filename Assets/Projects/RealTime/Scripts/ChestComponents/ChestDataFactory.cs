
namespace RealTime
{
    public sealed class ChestDataFactory
    {
        public ChestSaveData CreateChestData(ChestPresenter chestPresenter)
        {
            return new ChestSaveData(
                chestPresenter.ChestId,
                TextFormatter.DateTimeToString(chestPresenter.TimeToOpen),
                (int)chestPresenter.InitialTimer.TotalMinutes,
                chestPresenter.IsUnlocked);
        }
    }
}