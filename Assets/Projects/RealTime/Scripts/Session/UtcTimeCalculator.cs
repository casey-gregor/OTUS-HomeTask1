using System;
using UnityEngine;

namespace RealTime
{
    public sealed class UtcTimeCalculator
    {
        private readonly SessionLogger _sessionLogger;

        public UtcTimeCalculator(SessionLogger sessionLogger)
        {
            _sessionLogger = sessionLogger;
        }

        public void CalculateUtcSessionStart(DateTime serverTime, TimeSpan duration)
        {
            DateTime sessionStartedUtc = serverTime.Subtract(duration);
            Debug.Log($"Session started at {sessionStartedUtc}");
            _sessionLogger.SetUtcSessionStartTime(sessionStartedUtc);
        }
        
    }
}