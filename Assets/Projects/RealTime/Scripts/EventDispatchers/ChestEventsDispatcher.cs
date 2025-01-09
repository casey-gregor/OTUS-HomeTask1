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
        private readonly ChestActivator _chestActivator;

        public ChestEventsDispatcher(
            ChestSaveLoader chestSaveLoader,
            ChestSpawner chestSpawner, 
            ApplyChestReward applyChestReward, 
            ChestButtonTracker chestButtonTracker, 
            ChestLocker chestLocker, 
            ChestTimerCalculator chestTimerCalculator, 
            ChestActivator chestActivator)
        {
            _chestSaveLoader = chestSaveLoader;
            _chestSpawner = chestSpawner;
            _applyChestReward = applyChestReward;
            _chestButtonTracker = chestButtonTracker;
            _chestLocker = chestLocker;
            _chestTimerCalculator = chestTimerCalculator;
            _chestActivator = chestActivator;

            _chestSpawner.SpawnedSavedChests += HandleSavedChestsSpawned;
            _chestTimerCalculator.OnChestUnlocked += HandleChestUnlocked;
            _chestLocker.OnChestLocked += HandleChestLocked;
            _chestButtonTracker.OnChestButtonPressed += HandleChestOpened;
        }

        private void HandleSavedChestsSpawned()
        {
            _chestTimerCalculator.CheckSpawnedChests();
        }

        private void HandleChestLocked(ChestPresenter chestPresenter)
        {
            _chestActivator.ActivateChest(
                (int)chestPresenter.InitialTimer.TotalMinutes, 
                chestPresenter);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleChestUnlocked(ChestPresenter chestPresenter)
        {
            _chestButtonTracker.AddToList(chestPresenter);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleChestOpened(ChestPresenter chestPresenter)
        {
            chestPresenter.OpenChest();
            _applyChestReward.ApplyReward(chestPresenter.Rewards);
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