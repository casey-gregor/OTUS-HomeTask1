using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class StartButton : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        private Button startButton;

        public event Action StartEvent;
        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError($"{this.name} is missing GameManager");
            startButton = GetComponentInChildren<Button>();
            if (startButton == null)
                Debug.LogError($"{this.name} is missing StartButton");                 
        }

        public void OnButtonClick()
        {
            startButton.gameObject.SetActive(false);
            StartEvent?.Invoke();
        }

    }
}