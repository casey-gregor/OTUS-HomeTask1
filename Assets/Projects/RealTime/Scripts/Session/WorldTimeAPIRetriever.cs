using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace RealTime
{
    public sealed class WorldTimeAPIRetriever : IServerTimeRetriever
    {
        public event Action OnServerConnectStarted;
        public event Action<DateTime, TimeSpan> OnServerTimeReceived;

        private readonly string utcTimeURL = "http://worldtimeapi.org/api/timezone/etc/utc";
        private readonly CancellationTokenSource _cancellationToken = new();
        
        public void RetrieveServerTime()
        {
            ConnectToServer().Forget();
        }
        
        public async UniTask ConnectToServer()
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
                        ConvertServerResponse(responseText, serverResponseDuration);
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
        
        private void ConvertServerResponse(string serverTimeText, TimeSpan responseDuration)
        {
            ServerTimeData serverTimeData = JsonConvert.DeserializeObject<ServerTimeData>(serverTimeText);
            DateTime serverCurrentUtcTime = TextFormatter.StringToDateTimeUtcNonStrict(serverTimeData.utc_datetime);
            
            OnServerTimeReceived?.Invoke(serverCurrentUtcTime, responseDuration);
        }

        public void CancelServerTimeRequest()
        {
            _cancellationToken.Cancel();
        }
    }
}