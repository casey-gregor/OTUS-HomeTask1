using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class PlayPauseButton : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private GameManager gameManager;
        private TextMeshProUGUI textMeshPro;
        private Transform button;

        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError($"{this.name} is missing GameManager");
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro == null)
                Debug.LogError($"{this.name} is missing TextMesh component");
            button = transform.GetChild(0);
            if (button.GetComponent<Button>() == null)
                Debug.LogError($"{this.name} is missing Button");
            button.gameObject.SetActive(false);
        }

        private void Start()
        {
            IGameListener.Register(this);
        }

        void Update()
        {
            //SwitchText(gameManager.state);
        }

        void SwitchText(GameManager.State state)
        {
            switch(state)
            {
                case GameManager.State.Start: textMeshPro.text = "Pause"; break;
                case GameManager.State.Resume: textMeshPro.text = "Pause"; break;
                case GameManager.State.Pause: textMeshPro.text = "Resume"; break;
            }
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
    }

}
