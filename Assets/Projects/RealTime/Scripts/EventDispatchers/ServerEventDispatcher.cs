using System;
using Zenject;

namespace RealTime
{
    public class ServerEventDispatcher : ILateDisposable
    {
        private readonly SessionController _sessionController;
        private readonly IServerTimeRetriever _serverTimeRetriever;
        private readonly SessionDataManager _sessionDataManager;
        private readonly SessionPresenter _sessionPresenter;
        private readonly ServerConnectPresenter _serverConnectPresenter;

        public ServerEventDispatcher(
            SessionController sessionController, 
            IServerTimeRetriever serverTimeRetriever, 
            SessionDataManager sessionDataManager, 
            SessionPresenter sessionPresenter, 
            ServerConnectPresenter serverConnectPresenter)
        {
            _sessionController = sessionController;
            _serverTimeRetriever = serverTimeRetriever;
            _sessionDataManager = sessionDataManager;
            _sessionPresenter = sessionPresenter;
            _serverConnectPresenter = serverConnectPresenter;
            
            _serverTimeRetriever.OnServerConnectStarted += HandleWorldConnectStarted;
            _serverTimeRetriever.OnServerTimeReceived += HandleOnServerTimeReceived;
            _sessionDataManager.OnCurrentSessionStartSaved += HandleOnCurrentSessionStart;
            _sessionDataManager.OnPreviousSessionStartLoaded += HandleOnPreviousSessionStart;
            _sessionDataManager.OnPreviousSessionDurationLoaded += HandleOnPreviousSessionDuration;
        }

        private void HandleServerResponseProcessed(DateTime serverCurrentUtcTime, TimeSpan responseDuration)
        {
            _sessionController.CalculateUtcSessionStart(serverCurrentUtcTime, responseDuration);
        }

        private void HandleOnPreviousSessionDuration(TimeSpan time)
        {
            _sessionPresenter.HandleOnPreviousSessionDurationLoaded(time);
        }

        private void HandleOnPreviousSessionStart(DateTime dateTime)
        {
            _sessionPresenter.HandleLoadPreviousSessionStartEvent(dateTime);
        }

        private void HandleOnCurrentSessionStart(DateTime dateTime)
        {
            _sessionPresenter.HandleLoadCurrentSessionStartEvent(dateTime);
        }
        
        private void HandleWorldConnectStarted()
        {
            _serverConnectPresenter.ShowConnectingMessage();
        }

        private void HandleOnServerTimeReceived(DateTime dateTime, TimeSpan responseDuration)
        {
            _serverConnectPresenter.ShowConnectedMessage();
            _sessionController.CalculateUtcSessionStart(dateTime, responseDuration);
        }

        public void LateDispose()
        {
            _serverTimeRetriever.OnServerConnectStarted -= HandleWorldConnectStarted;
            _sessionDataManager.OnCurrentSessionStartSaved -= HandleOnCurrentSessionStart;
            _sessionDataManager.OnPreviousSessionStartLoaded -= HandleOnPreviousSessionStart;
            _sessionDataManager.OnPreviousSessionDurationLoaded -= HandleOnPreviousSessionDuration;
        }
    }
}