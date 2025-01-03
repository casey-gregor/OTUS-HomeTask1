using System;
using Cysharp.Threading.Tasks;

namespace RealTime
{
    public interface IServerTimeRetriever
    {
        public event Action OnServerConnectStarted;
        public event Action<DateTime, TimeSpan> OnServerTimeReceived;
        public void RetrieveServerTime();
    }
}