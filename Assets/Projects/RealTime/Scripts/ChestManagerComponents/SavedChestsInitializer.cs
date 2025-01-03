using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTime
{
    public class SavedChestsInitializer
    {
        private SessionController _sessionController;
        
        private TimeSpan _defaultTimeout = TimeSpan.FromMinutes(60);

        public SavedChestsInitializer(SessionController sessionController)
        {
            _sessionController = sessionController;
        }

        public void InitializeSavedChest(
            ChestModel chestModel,
            string chestId,
            List<IReward> rewards,
            int initialTimer,
            string receivedTime,
            string timeToOpen,
            bool isUnlocked,
            int currentTimer)
        {
            chestModel.SetId(chestId);
            chestModel.GetChestPresenter().SetViewPanelTitle(chestId);
            chestModel.SetReceivedTime(TextFormatter.StringToDateTimeUtcStrict(receivedTime));
            chestModel.SetOpenTime(TextFormatter.StringToDateTimeUtcStrict(timeToOpen));
            chestModel.SetIsUnlocked(isUnlocked);
            chestModel.SetRewards(rewards);
            chestModel.SetInitialTimer(TimeSpan.FromMinutes(initialTimer));
            chestModel.SetCurrentTimer(TimeSpan.FromMinutes(currentTimer));
        }
        
    }
}