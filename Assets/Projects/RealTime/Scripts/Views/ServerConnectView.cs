using TMPro;
using UnityEngine;

namespace RealTime
{
    public class ServerConnectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        
        public void ShowMessage(string text, Color color)
        {
            message.color = color;
            message.text = text;
        }
    }
}