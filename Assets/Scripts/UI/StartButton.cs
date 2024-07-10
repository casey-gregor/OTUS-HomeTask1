using System;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class StartButton
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

    }
}