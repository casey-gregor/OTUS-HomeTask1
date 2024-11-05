using System;
using Newtonsoft.Json;
using UnityEngine;

namespace RealTime
{
    public sealed class ServerTimeProcessor
    {
        private readonly UtcTimeCalculator _utcTimeCalculator;

        public ServerTimeProcessor(UtcTimeCalculator utcTimeCalculator)
        {
            _utcTimeCalculator = utcTimeCalculator;
        }

        public void ProcessServerTime(string responseText, TimeSpan responseDuration)
        {
            ServerTimeData serverTimeData = JsonConvert.DeserializeObject<ServerTimeData>(responseText);
            DateTime serverTime = TextFormatter.StringToDateTimeUtcNonStrict(serverTimeData.utc_datetime);
            Debug.Log($"serverTime at {serverTime}");
            
            _utcTimeCalculator.CalculateUtcSessionStart(serverTime, responseDuration);
        }
    }
}