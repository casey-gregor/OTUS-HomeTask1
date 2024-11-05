using System;

namespace RealTime
{
    public sealed class SessionPresenter
    {
        private readonly LogView _logView;
        
        private DateTime _sessionStart;
        private TimeSpan _sessionDuration;
        
        public SessionPresenter(LogView logView)
        {
            _logView = logView;
        }

        public void HandleLoadCurrentSessionStartEvent(DateTime dateTime)
        {
            _sessionStart = dateTime;
            string sessionStartText = "This session started at : " + dateTime;
            _logView.ShowCurrentSessionStart(sessionStartText);
        }

        public void HandleOnPreviousSessionDurationLoaded(TimeSpan timeSpan)
        {
            _sessionDuration = timeSpan;
            string sessionDurationText = "Previous session duration (hh:mm:ss) : " + timeSpan.
                ToString(@"hh\:mm\:ss");
            _logView.ShowPreviousSessionDuration(sessionDurationText);
            
            CalculatePreviousSessionEnd(_sessionStart, _sessionDuration);
        }

        public void HandleLoadPreviousSessionStartEvent(DateTime dateTime)
        {
            _sessionStart = dateTime;
            string sessionStartText = "Previous session started at : " + dateTime;
            _logView.ShowPreviousSessionStart(sessionStartText);
            
        }

        private void CalculatePreviousSessionEnd(DateTime sessionStart, TimeSpan sessionDuration)
        {
            DateTime previousSessionEnd = sessionStart + sessionDuration;
            string previousSessionEndText = "Previous session ended at : " + previousSessionEnd;
            _logView.ShowPreviousSessionEnd(previousSessionEndText);
        }
    }
}