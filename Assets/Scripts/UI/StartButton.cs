using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private float timer;
        [SerializeField] private TextMeshProUGUI countDownText;

        private TextCountDownComponent countDownComponent;
        private Button startButton;
        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError($"{this.name} is missing GameManager");

            if (countDownText == null)
                Debug.LogError($"{this.name} is missing CountDownText");

            startButton = GetComponentInChildren<Button>();
            if (startButton == null)
                Debug.LogError($"{this.name} is missing StartButton");

            countDownComponent = new TextCountDownComponent(gameManager, this, countDownText);
            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 0);
            countDownText.fontSize = 20;           
        }

        public void OnButtonClick()
        {
            startButton.gameObject.SetActive(false);
            countDownText.gameObject.SetActive(true);
            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 0);
            countDownText.fontSize = 20;
            StartCoroutine(countDownComponent.CountDown(timer, 1, GameManager.State.Start, true));
        }

    }
}