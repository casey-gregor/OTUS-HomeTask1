using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace RealTime
{
    public sealed class LogManager : MonoBehaviour
    {
        private SessionController _sessionController;
        private SessionDataManager _sessionDataManager;
        private IServerTimeRetriever _serverTimeRetriever;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        [Inject]
        private void Construct(
            SessionController sessionController, 
            SessionDataManager sessionDataManager, 
            IServerTimeRetriever serverTimeRetriever)
        {
            _sessionController = sessionController;
            _sessionDataManager = sessionDataManager;
            _serverTimeRetriever = serverTimeRetriever;
        }

        private void Start()
        {
            _sessionDataManager.TryLoadSessionStartFromPrefs();
            _sessionDataManager.TryLoadSessionDurationFromPrefs();
            
            var startData = _sessionController.LogUserStartSession();
            _sessionDataManager.SaveSessionStartToPrefs(startData);
            
            var durationData = _sessionController.UpdateUserSessionDuration();
            _sessionDataManager.SaveSessionDurationToPrefs(durationData);

            SaveSessionDuration(_sessionController.SessionDuration, _cancellationTokenSource.Token).Forget();
            _serverTimeRetriever.RetrieveServerTime();
        }
        
        private void OnApplicationQuit()
        {
            _sessionController.UpdateUserSessionDuration();
        }

        private void Update()
        {
            _sessionController.UpdateUserSessionDuration();
        }

        private async UniTaskVoid SaveSessionDuration(TimeSpan duration, CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                _sessionDataManager.SaveSessionDurationToPrefs(duration);
                await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: cancelToken);
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }
        
    }
}