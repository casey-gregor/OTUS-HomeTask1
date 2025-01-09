using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestModel
    {
        public string ChestId { get; private set; }
        public List<IReward> Rewards { get; private set; }
        public DateTime TimeToOpen { get; private set; }
        public TimeSpan InitialTimer {get; private set;}
        public TimeSpan CurrentTimer { get; private set; }
        public bool IsUnlocked { get; private set; }

        public void SetData(string chestId, List<IReward> rewards)
        {
            ChestId = chestId;
            Rewards = rewards;
        }
        
        public void SetIsUnlocked(bool value)
        {
            IsUnlocked = value;
        }
        
        public void SetOpenTime(DateTime value)
        {
            TimeToOpen = value;
        }

        public void SetInitialTimer(TimeSpan value)
        {
            InitialTimer = value;
        }
    }
}