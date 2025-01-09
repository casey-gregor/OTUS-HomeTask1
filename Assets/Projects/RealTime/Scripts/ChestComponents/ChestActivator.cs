using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestActivator
    {
        private readonly SessionController _sessionController;

        public ChestActivator(SessionController sessionController)
        {
            _sessionController = sessionController;
        }

        public void ActivateChest(int minutes, ChestPresenter chestPresenter)
        {
            var utcTime = _sessionController.GetAccurateUtcTime();
            chestPresenter.SetOpenTime(utcTime.AddMinutes(minutes));
            chestPresenter.SetInitialTimer(TimeSpan.FromMinutes(minutes));
        }
        
        public void SetChestData(
            ChestPresenter chestPresenter, 
            string chestId, 
            List<IReward> rewards)
        {
            chestPresenter.InitializeChestData(chestId, rewards);
        }
    }
}