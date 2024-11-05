using System;

namespace RealTime
{
    public class SessionDataManager
    {
        private readonly ISessionSaveLoader _sessionSaveLoader;
        
        public event Action<DateTime> OnCurrentSessionStartSaved;
        public event Action<DateTime> OnPreviousSessionStartLoaded;
        public event Action<TimeSpan> OnPreviousSessionDurationLoaded;

        public SessionDataManager(ISessionSaveLoader sessionSaveLoader)
        {
            _sessionSaveLoader = sessionSaveLoader;
        }

        private const string SESSION_START_TIME = "SessionStartTime";
        private const string SESSION_DURATION = "SessionDuration";
        
        public void SaveSessionStartToPrefs(DateTime data)
        {
            _sessionSaveLoader.SaveString(SESSION_START_TIME, data.ToString("o"));
            OnCurrentSessionStartSaved?.Invoke(data);
        }

        public void SaveSessionDurationToPrefs(TimeSpan data)
        {
            _sessionSaveLoader.SaveString(SESSION_DURATION, data.ToString());
        }

        public bool TryLoadSessionStartFromPrefs()
        {
            if (_sessionSaveLoader.HasKey(SESSION_START_TIME))
            {
                string sessionStartTime = _sessionSaveLoader.LoadString(SESSION_START_TIME);
                OnPreviousSessionStartLoaded?.Invoke(DateTime.Parse(sessionStartTime));
                return true;
            }

            return false;
        }

        public TimeSpan TryLoadSessionDurationFromPrefs()
        {
            if (_sessionSaveLoader.HasKey(SESSION_DURATION))
            {
                var sessionDuration = TimeSpan.Parse(_sessionSaveLoader.LoadString(SESSION_DURATION));
                OnPreviousSessionDurationLoaded?.Invoke(sessionDuration);
                return sessionDuration;
            }
            return TimeSpan.Zero;
        }
    }
}