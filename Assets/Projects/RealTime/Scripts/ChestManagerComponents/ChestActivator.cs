using UnityEngine;

namespace RealTime
{
    public sealed class ChestActivator
    {
        private readonly SessionLogger _sessionLogger;

        public ChestActivator(SessionLogger sessionLogger)
        {
            _sessionLogger = sessionLogger;
        }

        public void ActivateChest(int minutes, Chest chest)
        {
            chest.SetReceivedTime(_sessionLogger.UtcSessionStartTime.Add(_sessionLogger.SessionDuration));
            chest.SetOpenTime(chest.ReceivedTime.AddMinutes(minutes));
            Debug.Log("chest activated");
        }
    }
}