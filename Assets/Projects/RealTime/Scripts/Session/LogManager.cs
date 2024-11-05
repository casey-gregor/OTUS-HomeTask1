using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace RealTime
{
    public sealed class LogManager : MonoBehaviour
    {
        private SessionLogger _sessionLogger;
        private SessionDataManager _sessionDataManager;
        private ServerTimeGetter _serverTimeGetter;

        [Inject]
        private void Construct(
            SessionLogger sessionLogger, 
            SessionDataManager sessionDataManager, 
            ServerTimeGetter serverTimeGetter)
        {
            _sessionLogger = sessionLogger;
            _sessionDataManager = sessionDataManager;
            _serverTimeGetter = serverTimeGetter;
        }

        private void Start()
        {
            _sessionDataManager.TryLoadSessionStartFromPrefs();
            _sessionDataManager.TryLoadSessionDurationFromPrefs();
            
            var startData = _sessionLogger.LogStartSession();
            _sessionDataManager.SaveSessionStartToPrefs(startData);
            
            var durationData = _sessionLogger.UpdateSessionDuration();
            _sessionDataManager.SaveSessionDurationToPrefs(durationData);
            
            StartCoroutine(UpdateSessionDuration());
            _serverTimeGetter.GetServerTime().Forget();
        }
        
        private void OnApplicationQuit()
        {
            _sessionLogger.UpdateSessionDuration();
        }

        private IEnumerator UpdateSessionDuration()
        {
            yield return new WaitForSeconds(10);
            var data = _sessionLogger.UpdateSessionDuration();
            _sessionDataManager.SaveSessionDurationToPrefs(data);
            StartCoroutine(UpdateSessionDuration());
        }
    }
}