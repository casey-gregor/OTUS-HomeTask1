using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestLockChecker
    {
        public event Action<Chest> OnChestUnlocked;
        private readonly ChestSpawner _chestSpawner;
        private readonly SessionLogger _sessionLogger;
        
        private bool _launchChecker;

        public ChestLockChecker(
            ChestSpawner chestSpawner, 
            SessionLogger sessionLogger)
        {
            _chestSpawner = chestSpawner;
            _sessionLogger = sessionLogger;
        }

        public void HandleUtcTimeReceivedEvent(DateTime _)
        {
            if (!_launchChecker && _chestSpawner.SpawnedChests.Count > 0)
            {
                RunChestChecker().Forget();
                _launchChecker = true;
            }
        }

        public async UniTask RunChestChecker()
        {
            while (!_chestSpawner.SpawnedChests.All(chest => chest.IsUnlocked))
            {
                CheckChests();
            
                await UniTask.Delay(TimeSpan.FromMinutes(1)); 
            }
        }

        private void CheckChests()
        {
            Debug.Log("Checking chests");
            DateTime currentTime = _sessionLogger.UtcSessionStartTime.Add(_sessionLogger.SessionDuration);
            foreach (Chest chest in _chestSpawner.SpawnedChests)
            {
                if (chest.TimeToOpen <= currentTime && !chest.IsUnlocked)
                {
                    chest.SetIsUnlocked(true);
                    OnChestUnlocked?.Invoke(chest);
                }
            }
        }
    }
}