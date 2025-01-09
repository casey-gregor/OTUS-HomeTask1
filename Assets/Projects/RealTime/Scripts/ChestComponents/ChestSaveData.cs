using System;
using Newtonsoft.Json;

namespace RealTime
{
    [Serializable]
    public sealed class ChestSaveData
    {
        [JsonProperty("chest_id")]
        public string ChestId { get; set; }
        
        [JsonProperty("time_to_open")]
        public string TimeToOpen { get; set; }
        
        [JsonProperty("initial_timer_minutes")]
        public int InitialTimerMinutes { get; set; }
        
        [JsonProperty("is_unlocked")]
        public bool IsUnlocked { get; set; }
        

        public ChestSaveData(
            string chestId, 
            string timeToOpen,
            int initialTimerMinutes,
            bool isUnlocked
            )
        {
            ChestId = chestId;
            TimeToOpen = timeToOpen;
            InitialTimerMinutes = initialTimerMinutes;
            IsUnlocked = isUnlocked;
        }
    }
}