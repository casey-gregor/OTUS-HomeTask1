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

            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 0);
            countDownText.fontSize = 20;

           
        }

        public void OnButtonClick()
        {
            
            startButton.gameObject.SetActive(false);
            //Debug.Log("button clicked");
            countDownText.gameObject.SetActive(true);
            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 0);
            countDownText.fontSize = 20;
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            float countdown = timer;
            float interval = 1f;
            while (countdown > 0)
            {
                int seconds = Mathf.CeilToInt(countdown);
                StartCoroutine(AnimateText(seconds));
                yield return new WaitForSeconds(interval);
                countdown -= interval;
            }
            gameManager.SetState(GameManager.State.Start);
        }

        private IEnumerator AnimateText(int num)
        {
            countDownText.text = num.ToString();
            float duration = 1f;
            float halfDuration = duration / 2;

            for(float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t/ halfDuration;
                countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, progress);
                countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                yield return null;
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 1-progress);
                yield return null;
            }

            countDownText.color = new Color(countDownText.color.r, countDownText.color.g, countDownText.color.b, 0);
        }
    }
}