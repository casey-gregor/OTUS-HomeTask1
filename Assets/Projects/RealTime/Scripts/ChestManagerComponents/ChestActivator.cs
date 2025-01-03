
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestActivator
    {
        private readonly SessionController _sessionController;

        public ChestActivator(SessionController sessionController)
        {
            _sessionController = sessionController;
        }

        public void ActivateChest(int minutes, ChestModel chestModel)
        {
            var utcTime = _sessionController.GetAccurateUtcTime();
            chestModel.SetReceivedTime(utcTime);
            chestModel.SetOpenTime(chestModel.ReceivedTime.AddMinutes(minutes));
            chestModel.SetInitialTimer(TimeSpan.FromMinutes(minutes));
            Debug.Log("activated with mins : " + minutes);
        }
        
        public void SetChestData(
            ChestModel chestModel, 
            string chestId, 
            List<IReward> rewards)
        {
            chestModel.GetChestPresenter().SetViewPanelTitle(chestId);
            chestModel.SetId(chestId);
            chestModel.SetRewards(rewards);
        }
    }
}