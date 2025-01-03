using System;
using Cysharp.Threading.Tasks;

namespace RealTime
{
    public class TestServerTimeRetriever : IServerTimeRetriever
    {
        public event Action OnServerConnectStarted;
        public event Action<DateTime, TimeSpan> OnServerTimeReceived;

        public void RetrieveServerTime()
        { 
            OnServerConnectStarted?.Invoke();
            DelayConnection().Forget();
        }

        private async UniTaskVoid DelayConnection()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10));
            OnServerTimeReceived?.Invoke(DateTime.Now, TimeSpan.FromSeconds(10));
        }
    }
}