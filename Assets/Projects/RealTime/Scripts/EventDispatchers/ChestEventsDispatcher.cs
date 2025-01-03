using System;
using Zenject;

namespace RealTime
{
    public class ChestEventsDispatcher : ILateDisposable
    {
        private readonly IChestSaveLoader _chestSaveLoader;
        private readonly ChestLockChecker _chestLockChecker;
        private readonly ChestLocker _chestLocker;
        private readonly ChestSpawner _chestSpawner;
        private readonly ApplyChestReward _applyChestReward;
        private readonly ChestButtonTracker _chestButtonTracker;
        private readonly ChestTimerCalculator _chestTimerCalculator;
        private readonly ChestManager _chestManager;

        public ChestEventsDispatcher(
            ChestSaveLoader chestSaveLoader,
            ChestLockChecker chestLockChecker,
            ChestSpawner chestSpawner, 
            ApplyChestReward applyChestReward, 
            ChestButtonTracker chestButtonTracker, 
            ChestLocker chestLocker, 
            ChestTimerCalculator chestTimerCalculator,
            ChestManager chestManager)
        {
            _chestSaveLoader = chestSaveLoader;
            _chestLockChecker = chestLockChecker;
            _chestSpawner = chestSpawner;
            _applyChestReward = applyChestReward;
            _chestButtonTracker = chestButtonTracker;
            _chestLocker = chestLocker;
            _chestTimerCalculator = chestTimerCalculator;

            _chestManager = chestManager;

            _chestSpawner.SpawnedSavedChests += HandleSavedChestsSpawned;
            _chestManager.OnChestSpawned += HandleChestSpawned;
            _chestTimerCalculator.OnChestUnlocked += HandleChestUnlocked;
            _chestLocker.OnChestLocked += HandleChestLocked;
            _chestButtonTracker.OnChestButtonPressed += HandleChestOpened;
        }

        private void HandleSavedChestsSpawned()
        {
            _chestTimerCalculator.CheckSpawnedChests();
        }

        private void HandleChestSpawned()
        {
            // _chestLockChecker.StartChecking(DateTime.Now);
        }

        private void HandleChestLocked()
        {
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleChestUnlocked(ChestModel chestView)
        {
            _chestButtonTracker.AddToList(chestView);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleChestOpened(ChestModel chestModel)
        {
            chestModel.GetChestPresenter().OpenChest();
            _applyChestReward.ApplyReward(chestModel.Rewards);
            _chestLocker.InitiateChestLock(chestModel);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        public void LateDispose()
        {
            _chestLockChecker.OnChestUnlocked -= HandleChestUnlocked;
            _chestButtonTracker.OnChestButtonPressed -= HandleChestOpened;
        }
    }
}