using System;

namespace RealTime
{
    public class ChestInitializer
    {
        public void InitializeChest(Chest chest, ChestData chestData)
        {
            chest.SetReceivedTime(TextFormatter.StringToDateTimeUtcStrict(chestData.ReceivedTime));
            chest.SetOpenTime(TextFormatter.StringToDateTimeUtcStrict(chestData.TimeToOpen));
            chest.SetTimerMinutes(TimeSpan.FromMinutes(chestData.TimerMinutes));
            chest.SetIsUnlocked(chestData.IsUnlocked);
        }
    }
}