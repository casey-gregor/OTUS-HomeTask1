using System;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class StartButton : IDisposable
    {
        public event Action StartEvent;
        
        private Button startButton;


        public StartButton(Button startButton)
        {
            this.startButton = startButton;
            this.startButton.onClick.AddListener(OnButtonClick);
            
        }

        private void OnButtonClick()
        {
            this.startButton.gameObject.SetActive(false);
            this.StartEvent?.Invoke();
        }

        public void Dispose()
        {
            this.startButton.onClick.RemoveListener(OnButtonClick);
        }
    }
}