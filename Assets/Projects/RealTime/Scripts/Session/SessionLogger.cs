using System;
using UnityEngine;

namespace RealTime
{
    public sealed class SessionLogger
    {
        public event Action<DateTime> OnUtcTimeReceived;
        public DateTime UserSessionStartTime { get; private set; }
        public DateTime UtcSessionStartTime { get; private set; }
        public TimeSpan SessionDuration { get; private set; }
        public bool GotServerData { get; private set; }
        
        
        public DateTime LogStartSession()
        {
            UserSessionStartTime = DateTime.Now;
            UtcSessionStartTime = DateTime.UtcNow;
            GotServerData = false;
            return UserSessionStartTime;
        }

        public TimeSpan UpdateSessionDuration()
        {
            SessionDuration = DateTime.Now - UserSessionStartTime;
            return SessionDuration;
        }

        public void SetUtcSessionStartTime(DateTime utcSessionStartTime)
        {
            UtcSessionStartTime = utcSessionStartTime;
            Debug.Log("UTC start time updated : " + UtcSessionStartTime);
            GotServerData = true;
            OnUtcTimeReceived?.Invoke(UtcSessionStartTime);
        }
    }
}