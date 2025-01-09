using System;
using Zenject;

namespace RealTime
{
    public sealed class ChestTimerCalculator : ITickable
    {
        public event Action<ChestPresenter> OnChestUnlocked;
        private readonly ChestSpawner _chestSpawner;
        private readonly SessionController _sessionController;

        public ChestTimerCalculator(ChestSpawner chestSpawner, SessionController sessionController)
        {
            _chestSpawner = chestSpawner;
            _sessionController = sessionController;
        }

        public void CheckSpawnedChests()
        {
            foreach (ChestPresenter chestPresenter in _chestSpawner.SpawnedChests)
            {
                if (chestPresenter.IsUnlocked)
                {
                    SetTimerText(chestPresenter, TimeSpan.Zero);
                    OnChestUnlocked?.Invoke(chestPresenter);
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
            
            foreach (ChestPresenter chestPresenter in _chestSpawner.SpawnedChests)
            {
                if (chestPresenter.IsUnlocked)
                {
                    continue;
                }
                DateTime currentUtcTime = _sessionController.GetAccurateUtcTime();
                TimeSpan timeLeft =  chestPresenter.TimeToOpen - currentUtcTime;
                if (timeLeft <= TimeSpan.Zero && !chestPresenter.IsUnlocked)
                {
                    SetTimerText(chestPresenter, TimeSpan.Zero);
                    chestPresenter.SetIsUnlocked(true);
                    OnChestUnlocked?.Invoke(chestPresenter);
                    return;
                }
                SetTimerText(chestPresenter, timeLeft);
            }
        }
        
        private void SetTimerText(ChestPresenter chestPresenter, TimeSpan timer)
        {
            chestPresenter.UpdateChestTimer(timer);
        }
    }
}