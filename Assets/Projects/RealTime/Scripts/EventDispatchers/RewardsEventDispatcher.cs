using System;
using RealTime.Rewards;

namespace RealTime
{
    public sealed class RewardsEventDispatcher : IDisposable
    {
        private readonly RewardsPopupController _rewardsPopupController;
        private readonly RewardsPresenter _rewardsPresenter;
        private readonly ChestButtonTracker _chestButtonTracker;
        private readonly ChestLocker _chestLocker;

        public RewardsEventDispatcher(
            ChestButtonTracker chestButtonTracker,
            RewardsPopupController rewardsPopupController, 
            ChestLocker chestLocker, 
            RewardsPresenter rewardsPresenter)
        {
            _chestButtonTracker = chestButtonTracker;
            _rewardsPopupController = rewardsPopupController;
            _chestLocker = chestLocker;
            _rewardsPresenter = rewardsPresenter;

            _chestButtonTracker.OnChestButtonPressed += _rewardsPopupController.ShowRewards;
            _rewardsPopupController.OnRewardInstantiated += _rewardsPresenter.RewardInstantiated;
            _rewardsPopupController.OnRewardInstantiated += HandleRewardInstantiated;
        }
        
        public void Dispose()
        {
            _chestButtonTracker.OnChestButtonPressed -= _rewardsPopupController.ShowRewards;
            _rewardsPopupController.OnRewardInstantiated -= _rewardsPresenter.RewardInstantiated;
            _rewardsPopupController.OnRewardInstantiated -= HandleRewardInstantiated;
        }

        private void HandleRewardInstantiated(RewardsView rewardsView, ChestPresenter chestPresenter)
        {
            rewardsView.OnCloseButtonClicked += InitiateClose;
        }

        private void InitiateClose(RewardsView rewardsView)
        {
            rewardsView.OnCloseButtonClicked -= InitiateClose;
            if (_rewardsPopupController.RewardsDictionary.TryGetValue(rewardsView, out ChestPresenter chestPresenter))
            {
                _chestLocker.InitiateChestLock(chestPresenter);
                _rewardsPopupController.CloseRewards(rewardsView);
            }
        }
    }
}