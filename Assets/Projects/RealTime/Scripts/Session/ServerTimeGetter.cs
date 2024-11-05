using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace RealTime
{
    public sealed class ServerTimeGetter
    {
        public event Action OnServerConnectStarted;
        public event Action<string, TimeSpan> OnServerTimeReceived;
        
        private readonly string utcTimeURL = "http://worldtimeapi.org/api/timezone/etc/utc";
        private readonly CancellationTokenSource _cancellationToken = new();
        
        public async UniTask GetServerTime()
        {
            DateTime start = DateTime.Now;

            bool success = false;
            OnServerConnectStarted?.Invoke();
            
            while (!success && !_cancellationToken.Token.IsCancellationRequested)
            {
                UnityWebRequest request = UnityWebRequest.Get(utcTimeURL);
        
                try
                {
                    await request.SendWebRequest();
            
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        success = true; 

                        string responseText = request.downloadHandler.text;
                        TimeSpan serverResponseDuration = DateTime.Now - start;
                        
                        OnServerTimeReceived?.Invoke(responseText, serverResponseDuration);
                    }
                    else
                    {
                        Debug.LogError($"Failed to get server time: {request.error}");
                        await UniTask.Delay(1000); 
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception occurred: {ex.Message}");
                    await UniTask.Delay(1000);
                }
            }
        }

        public void CancelServerTimeRequest()
        {
            _cancellationToken.Cancel();
        }
    }
}