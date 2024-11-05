using System;
using Zenject;

namespace RealTime
{
    public class EventDispatcher : ILateDisposable
    {
        private readonly IChestSaveLoader _chestSaveLoader;
        private readonly ChestLockChecker _chestLockChecker;
        private readonly ChestLocker _chestLocker;
        private readonly ChestSpawner _chestSpawner;
        private readonly SessionLogger _sessionLogger;
        private readonly ApplyChestReward _applyChestReward;
        private readonly ServerTimeGetter _serverTimeGetter;
        private readonly ServerTimeProcessor _serverTimeProcessor;
        private readonly SessionDataManager _sessionDataManager;
        private readonly SessionPresenter _sessionPresenter;
        private readonly ServerConnectPresenter _serverConnectPresenter;
        private readonly ChestButtonTracker _chestButtonTracker;

        public EventDispatcher(
            ChestSaveLoader chestSaveLoader,
            ChestLockChecker chestLockChecker,
            ChestSpawner chestSpawner, 
            SessionLogger sessionLogger, 
            ApplyChestReward applyChestReward, 
            ServerTimeGetter serverTimeGetter, 
            ServerTimeProcessor serverTimeProcessor, 
            SessionDataManager sessionDataManager, 
            SessionPresenter sessionPresenter, 
            ServerConnectPresenter serverConnectPresenter, 
            ChestButtonTracker chestButtonTracker, 
            ChestLocker chestLocker)
        {
            _chestSaveLoader = chestSaveLoader;
            _chestLockChecker = chestLockChecker;
            _chestSpawner = chestSpawner;
            _sessionLogger = sessionLogger;
            _applyChestReward = applyChestReward;
            _serverTimeGetter = serverTimeGetter;
            _serverTimeProcessor = serverTimeProcessor;
            _sessionDataManager = sessionDataManager;
            _sessionPresenter = sessionPresenter;
            _serverConnectPresenter = serverConnectPresenter;
            _chestButtonTracker = chestButtonTracker;
            _chestLocker = chestLocker;

            _sessionLogger.OnUtcTimeReceived += HandleUtcTimeReceivedEvent;
            _chestLockChecker.OnChestUnlocked += HandleChestUnlocked;
            _chestLocker.OnChestLocked += HandleChestLocked;
            _chestButtonTracker.OnChestButtonPressed += HandleChestOpened;
            _serverTimeGetter.OnServerConnectStarted += HandleServerConnectStarted;
            _serverTimeGetter.OnServerTimeReceived += HandleOnServerTimeGetterReceived;
            _sessionDataManager.OnCurrentSessionStartSaved += HandleOnCurrentSessionStart;
            _sessionDataManager.OnPreviousSessionStartLoaded += HandleOnPreviousSessionStart;
            _sessionDataManager.OnPreviousSessionDurationLoaded += HandleOnPreviousSessionDuration;
        }

        private void HandleChestLocked()
        {
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleOnPreviousSessionDuration(TimeSpan time)
        {
            _sessionPresenter.HandleOnPreviousSessionDurationLoaded(time);
        }

        private void HandleOnPreviousSessionStart(DateTime dateTime)
        {
            _sessionPresenter.HandleLoadPreviousSessionStartEvent(dateTime);
        }

        private void HandleOnCurrentSessionStart(DateTime dateTime)
        {
            _sessionPresenter.HandleLoadCurrentSessionStartEvent(dateTime);
        }
        
        private void HandleServerConnectStarted()
        {
            _serverConnectPresenter.ShowConnectingMessage();
        }

        private void HandleOnServerTimeGetterReceived(string responseText, TimeSpan responseDuration)
        {
            _serverConnectPresenter.ShowConnectedMessage();
            _serverTimeProcessor.ProcessServerTime(responseText, responseDuration);
        }

        private void HandleUtcTimeReceivedEvent(DateTime _)
        {
            _chestLockChecker.HandleUtcTimeReceivedEvent(_);
        }

        private void HandleChestUnlocked(Chest chest)
        {
            _chestButtonTracker.AddToList(chest);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
        }

        private void HandleChestOpened(Chest chest)
        {
            chest.OpenChest();
            _applyChestReward.ApplyReward(chest);
            _chestButtonTracker.RemoveFromList(chest);
            _chestSaveLoader.SaveChests(_chestSpawner.SpawnedChests);
            _chestLocker.LockChest(chest);
        }

        public void LateDispose()
        {
            _sessionLogger.OnUtcTimeReceived -= HandleUtcTimeReceivedEvent;
            _chestLockChecker.OnChestUnlocked -= HandleChestUnlocked;
            _chestButtonTracker.OnChestButtonPressed -= HandleChestOpened;
            _serverTimeGetter.OnServerConnectStarted -= HandleServerConnectStarted;
            _serverTimeGetter.OnServerTimeReceived -= HandleOnServerTimeGetterReceived;
            _sessionDataManager.OnCurrentSessionStartSaved -= HandleOnCurrentSessionStart;
            _sessionDataManager.OnPreviousSessionStartLoaded -= HandleOnPreviousSessionStart;
            _sessionDataManager.OnPreviousSessionDurationLoaded -= HandleOnPreviousSessionDuration;
        }
    }
}