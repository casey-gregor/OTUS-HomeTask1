using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestModel : IDisposable
    {
        public string ChestId { get; private set; }
        public List<IReward> Rewards { get; private set; }
        public DateTime TimeToOpen { get; private set; }
        public DateTime ReceivedTime { get; private set; }
        public TimeSpan InitialTimer {get; private set;}
        public TimeSpan CurrentTimer { get; private set; }
        public bool IsUnlocked { get; private set; }
        
        private readonly ChestPresenter _chestPresenter;

        public ChestModel(ChestPresenter chestPresenter)
        {
            _chestPresenter = chestPresenter;
        }

        public ChestPresenter GetChestPresenter()
        {
            return _chestPresenter;
        }

        public ChestView GetChestView()
        {
            return _chestPresenter.GetChestView();
        }
        
        public void SetId(string chestId)
        {
            ChestId = chestId;
        }

        public void SetRewards(List<IReward> bonuses)
        {
            Rewards = bonuses;
        }
        public void SetIsUnlocked(bool value)
        {
            IsUnlocked = value;
            if (IsUnlocked)
            {
                _chestPresenter.ToggleTimerVisibility(false);
                _chestPresenter.ToggleOpenButton(true);
            }
            else
            {
                _chestPresenter.ToggleOpenButton(false);
                _chestPresenter.ToggleTimerVisibility(true);
            }
        }

        public void SetReceivedTime(DateTime value)
        {
            ReceivedTime = value;
        }

        public void SetOpenTime(DateTime value)
        {
            TimeToOpen = value;
        }

        public void SetInitialTimer(TimeSpan value)
        {
            InitialTimer = value;
        }

        public void SetCurrentTimer(TimeSpan value)
        {
            CurrentTimer = value;
        }

        public void Dispose()
        {
            _chestPresenter.Dispose();
        }
    }
}