using Zenject;

namespace RealTime
{
    public class ChestEventsDispatcher : ILateDisposable
    {
        private readonly IChestSaveLoader _chestSaveLoader;
        private readonly ChestLocker _chestLocker;
        private readonly ChestSpawner _chestSpawner;
        private readonly ApplyChestReward _applyChestReward;
        private readonly ChestButtonTracker _chestButtonTracker;
        private readonly ChestTimerCalculator _chestTimerCalculator;

        public ChestEventsDispatcher(
            ChestSaveLoader chestSaveLoader,
            ChestSpawner chestSpawner, 
            ApplyChestReward applyChestReward, 
            ChestButtonTracker chestButtonTracker, 
            ChestLocker chestLocker, 
            ChestTimerCalculator chestTimerCalculator)
        {
            _chestSaveLoader = chestSaveLoader;
            _chestSpawner = chestSpawner;
            _applyChestReward = applyChestReward;
            _chestButtonTracker = chestButtonTracker;
            _chestLocker = chestLocker;
            _chestTimerCalculator = chestTimerCalculator;

            _chestSpawner.SpawnedSavedChests += HandleSavedChestsSpawned;
            _chestTimerCalculator.OnChestUnlocked += HandleChestUnlocked;
            _chestLocker.OnChestLocked += HandleChestLocked;
            _chestButtonTracker.OnChestButtonPressed += HandleChestOpened;
        }

        private void HandleSavedChestsSpawned()
        {
            _chestTimerCalculator.CheckSpawnedChests();
        }

        private void HandleChestLocked(ChestModel chestModel)
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
            _chestSpawner.SpawnedSavedChests -= HandleSavedChestsSpawned;
            _chestTimerCalculator.OnChestUnlocked -= HandleChestUnlocked;
            _chestLocker.OnChestLocked -= HandleChestLocked;
            _chestButtonTracker.OnChestButtonPressed -= HandleChestOpened;
        }
    }
}