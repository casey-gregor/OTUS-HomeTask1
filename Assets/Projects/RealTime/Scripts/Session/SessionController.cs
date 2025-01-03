using System;
using UnityEngine;

namespace RealTime
{
    public sealed class SessionController
    {
        public event Action<DateTime> OnUtcTimeReceived;
        public DateTime UserSessionStartTime { get; private set; }
        public DateTime UtcSessionStartTime { get; private set; }
        public TimeSpan SessionDuration { get; private set; }
        public bool GotServerTime { get; private set; }

        private TimeSpan _utcTimeOffset;

        public DateTime GetAccurateUtcTime()
        {
            return DateTime.UtcNow + _utcTimeOffset;
        }
        
        public DateTime LogUserStartSession()
        {
            UserSessionStartTime = DateTime.Now;
            GotServerTime = false;
            return UserSessionStartTime;
        }

        public TimeSpan UpdateUserSessionDuration()
        {
            SessionDuration = DateTime.Now - UserSessionStartTime;
            return SessionDuration;
        }
        
        public void CalculateUtcSessionStart(DateTime serverCurrentUtcTime, TimeSpan duration)
        {
            GotServerTime = true;
            UtcSessionStartTime = serverCurrentUtcTime.Subtract(duration);
            CalculateUtcOffset(userSessionStartTime: UserSessionStartTime, utcSessionStartTime: serverCurrentUtcTime);
            OnUtcTimeReceived?.Invoke(UtcSessionStartTime);
        }

        private void CalculateUtcOffset(DateTime userSessionStartTime, DateTime utcSessionStartTime)
        {
            _utcTimeOffset = userSessionStartTime - utcSessionStartTime;
        }
    }
}