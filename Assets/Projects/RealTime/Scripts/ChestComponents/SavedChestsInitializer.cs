using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class SavedChestsInitializer
    {
        public void InitializeSavedChest(
            ChestPresenter chestPresenter,
            string chestId,
            List<IReward> rewards,
            int initialTimer,
            string timeToOpen,
            bool isUnlocked)
        {
            chestPresenter.InitializeChestData(chestId, rewards);
            chestPresenter.SetIsUnlocked(isUnlocked);
            chestPresenter.SetOpenTime(TextFormatter.StringToDateTimeUtcStrict(timeToOpen));
            chestPresenter.SetInitialTimer(TimeSpan.FromMinutes(initialTimer));
        }
        
    }
}