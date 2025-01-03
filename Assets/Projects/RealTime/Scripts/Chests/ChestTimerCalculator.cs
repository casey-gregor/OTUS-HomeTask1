using System;
using UnityEngine;
using Zenject;

namespace RealTime
{
    public class ChestTimerCalculator : ITickable
    {
        public event Action<ChestModel> OnChestUnlocked;
        private readonly ChestSpawner _chestSpawner;
        private readonly SessionController _sessionController;

        public ChestTimerCalculator(ChestSpawner chestSpawner, SessionController sessionController)
        {
            _chestSpawner = chestSpawner;
            _sessionController = sessionController;
        }

        public void CheckSpawnedChests()
        {
            foreach (ChestModel chest in _chestSpawner.SpawnedChests)
            {
                if (chest.IsUnlocked)
                {
                    Debug.Log("chest is unlocked");
                    TimerText(chest, TimeSpan.Zero);
                    OnChestUnlocked?.Invoke(chest);
                    return;
                }
            }
            
        }

        public void Tick()
        {
            if (!_sessionController.GotServerTime)
            {
                return;
            }
            
            foreach (var chest in _chestSpawner.SpawnedChests)
            {
                if (chest.IsUnlocked)
                {
                    return;
                }
                DateTime currentUtcTime = _sessionController.GetAccurateUtcTime();
                TimeSpan timeLeft =  chest.TimeToOpen - currentUtcTime;
                // Debug.Log("timeleft : " + timeLeft);
                if (timeLeft <= TimeSpan.Zero && !chest.IsUnlocked)
                {
                    Debug.Log("chest unlocked");
                    TimerText(chest, TimeSpan.Zero);
                    chest.SetIsUnlocked(true);
                    OnChestUnlocked?.Invoke(chest);
                    return;
                }
                TimerText(chest, timeLeft);
            }
        }
        
        private void TimerText(ChestModel chestModel, TimeSpan timer)
        {
            chestModel.GetChestPresenter().UpdateChestTimer(timer);
        }
        
    }
}