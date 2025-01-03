using UnityEngine;

namespace RealTime
{
    public sealed class ChestDataFactory
    {
        public ChestSaveData CreateChestData(ChestModel chestModel)
        {
            string receivedTime = TextFormatter.DateTimeToString(chestModel.ReceivedTime);
            string timeToOpen = TextFormatter.DateTimeToString(chestModel.TimeToOpen);
            Debug.Log("is chest data factory. default timer : " + chestModel.InitialTimer);
            return new ChestSaveData(
                chestModel.ChestId, 
                receivedTime, 
                timeToOpen,
                chestModel.InitialTimer.Minutes,
                chestModel.CurrentTimer.Minutes,
                chestModel.IsUnlocked);;
        }
    }
}