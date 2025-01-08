using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealTime
{
    public class SavedChestsInitializer
    {
        public void InitializeSavedChest(
            ChestModel chestModel,
            string chestId,
            List<IReward> rewards,
            int initialTimer,
            string timeToOpen,
            bool isUnlocked)
        {
            chestModel.SetId(chestId);
            chestModel.GetChestPresenter().SetViewPanelTitle(chestId);
            chestModel.SetOpenTime(TextFormatter.StringToDateTimeUtcStrict(timeToOpen));
            chestModel.SetIsUnlocked(isUnlocked);
            chestModel.SetRewards(rewards);
            Debug.Log("initialTimer : " + initialTimer);
            chestModel.SetInitialTimer(TimeSpan.FromMinutes(initialTimer));
            Debug.Log("set initial timer : " + TimeSpan.FromMinutes(initialTimer));
        }
        
    }
}