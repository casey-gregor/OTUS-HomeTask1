using System;
using Newtonsoft.Json;

namespace RealTime
{
    [Serializable]
    public sealed class ChestData
    {
        [JsonProperty("chest_id")]
        public string ChestId { get; set; }
        
        [JsonProperty("received_time")]
        public string ReceivedTime { get; set; }
        
        [JsonProperty("time_to_open")]
        public string TimeToOpen { get; set; }
        
        [JsonProperty("timer_minutes")]
        public int TimerMinutes { get; set; }
        
        [JsonProperty("is_unlocked")]
        public bool IsUnlocked { get; set; }

        public ChestData(
            string chestId, 
            string receivedTime, 
            string timeToOpen,
            int timerMinutes,
            bool isUnlocked)
        {
            ChestId = chestId;
            ReceivedTime = receivedTime;
            TimeToOpen = timeToOpen;
            TimerMinutes = timerMinutes;
            IsUnlocked = isUnlocked;
        }
    }
}