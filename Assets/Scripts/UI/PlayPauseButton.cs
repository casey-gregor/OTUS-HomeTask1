using System;
using TMPro;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class PlayPauseButton : 
        IGameStartListener, 
        IGameFinishListener,
        IDisposable
    {
        private GameManager gameManager;
        private TextMeshProUGUI textMeshPro;
        private Button button;

        public PlayPauseButton(GameManager gameManager, TextMeshProUGUI textMeshPro, Button button)
        {
            this.gameManager = gameManager;
            this.textMeshPro = textMeshPro;
            this.button = button;

            this.button.onClick.AddListener(OnButtonClick);

            this.button.gameObject.SetActive(false);

        }

        public void OnStart()
        {
            button.gameObject.SetActive(true);
            textMeshPro.text = "Pause";
        }
        public void OnButtonClick()
        {
            switch (gameManager.state)
            {
                case GameManager.State.Start:
                    textMeshPro.text = "Resume";
                    gameManager.SetState(GameManager.State.Pause); 
                    break;
                case GameManager.State.Resume: 
                    textMeshPro.text = "Resume";
                    gameManager.SetState(GameManager.State.Pause);
                    break;
                case GameManager.State.Pause: 
                    textMeshPro.text = "Pause";
                    gameManager.SetState(GameManager.State.Resume);
                    break;
            }
        }

        public void OnFinish()
        {
            button.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            this.button.onClick.RemoveListener(OnButtonClick);
        }
    }

}
