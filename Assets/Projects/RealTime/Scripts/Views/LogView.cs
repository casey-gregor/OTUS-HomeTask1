using TMPro;
using UnityEngine;

namespace RealTime
{
    public class LogView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI previousSessionStart;
        [SerializeField] private TextMeshProUGUI previousSessionEnd;
        [SerializeField] private TextMeshProUGUI previousSessionDuration;
        [SerializeField] private TextMeshProUGUI currentSessionStart;
        

        public void ShowPreviousSessionStart(string sessionText)
        {
            previousSessionStart.text = sessionText;
        }
        public void ShowPreviousSessionEnd(string sessionText)
        {
            previousSessionEnd.text = sessionText;
        }

        public void ShowPreviousSessionDuration(string sessionText)
        {
            previousSessionDuration.text = sessionText;
        }

        public void ShowCurrentSessionStart(string sessionText)
        {
            currentSessionStart.text = sessionText;
        }
    }
}