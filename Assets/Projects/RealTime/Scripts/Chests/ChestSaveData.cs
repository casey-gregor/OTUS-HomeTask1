using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RealTime
{
    [Serializable]
    public sealed class ChestSaveData
    {
        [JsonProperty("chest_id")]
        public string ChestId { get; set; }
        
        [JsonProperty("received_time")]
        public string ReceivedTime { get; set; }
        
        [JsonProperty("time_to_open")]
        public string TimeToOpen { get; set; }
        
        [JsonProperty("initial_timer_minutes")]
        public int InitialTimerMinutes { get; set; }
        
        [JsonProperty("current_timer_minutes")]
        public int CurrentTimer { get; set; }
        
        [JsonProperty("is_unlocked")]
        public bool IsUnlocked { get; set; }
        

        public ChestSaveData(
            string chestId, 
            string receivedTime, 
            string timeToOpen,
            int initialTimerMinutes,
            int currentTimer,
            bool isUnlocked
            )
        {
            ChestId = chestId;
            ReceivedTime = receivedTime;
            TimeToOpen = timeToOpen;
            InitialTimerMinutes = initialTimerMinutes;
            CurrentTimer = currentTimer;
            IsUnlocked = isUnlocked;
        }
    }
}