using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestLockChecker
    {
        public event Action<ChestModel> OnChestUnlocked;
        private readonly ChestSpawner _chestSpawner;
        private readonly SessionController _sessionController;
        
        private bool _checkerIsRunning;
        private bool _notifyAllChestsUnlocked;

        public ChestLockChecker(
            ChestSpawner chestSpawner, 
            SessionController sessionController)
        {
            _chestSpawner = chestSpawner;
            _sessionController = sessionController;
        }

        // public void StartChecking(DateTime _)
        // {
        //     if (!_checkerIsRunning && _chestSpawner.SpawnedChests.Count > 0)
        //     {
        //         _notifyAllChestsUnlocked = true;
        //         _checkerIsRunning = true;
        //         RunChestChecker().Forget();
        //     }
        // }

        public async UniTask RunChestChecker()
        {
            while (!_chestSpawner.SpawnedChests.All(chest => chest.IsUnlocked))//Is going to run checks until all chests are unlocked;
            {
                Debug.Log("Waiting for chest to unlock");
                _notifyAllChestsUnlocked = false;
                CheckLockedChests();
                await UniTask.Delay(TimeSpan.FromMinutes(1));
            }

            if (_notifyAllChestsUnlocked)//Needs to run once on startup to subscribe all chests' open button to open event;
            {
                Debug.Log("checking unlock on startup");
                NotifyAllChestsUnlocked();
                _notifyAllChestsUnlocked = false;
            }
        }

        private void CheckLockedChests()
        {
            // _sessionController.GetCurrentUtcTime(out var currentUtcTime);
            // foreach (Chest chest in _chestSpawner.SpawnedChests)
            // {
            //     TimeSpan timeLeft = currentUtcTime - chest.TimeToOpen;
            //     if (timeLeft <= TimeSpan.Zero && !chest.IsUnlocked)
            //     {
            //         Debug.Log("chest unlocked");
            //         chest.SetIsUnlocked(true);
            //         OnChestUnlocked?.Invoke(chest);
            //     }
            //     else
            //     {
            //         chest.CountdownTimer(timeLeft);
            //     }
            // }
        }

        private void NotifyAllChestsUnlocked()
        {
            foreach (ChestModel chest in _chestSpawner.SpawnedChests)
            {
                OnChestUnlocked?.Invoke(chest);
            }
        }
    }
}