using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class TextCountDownComponent : MonoBehaviour
    {
        [SerializeField] float timer;
        [SerializeField] int interval = 1;
        [SerializeField] GameManager.State stateToSet = GameManager.State.Start;
        [SerializeField] bool isTextAnimatable;
        [SerializeField] StartButton startButton;

        [Inject] private GameManager gameManager;
        private TextMeshProUGUI countDownText;

        private void Awake()
        {
            countDownText = GetComponentInChildren<TextMeshProUGUI>();
            if (countDownText == null)
                Debug.LogError($"{this.name} is missing TextMeshPro component");
            countDownText.fontSize = 20;
            countDownText.enabled = false;
            startButton.StartEvent += HandleStartEvent;
        }

        public IEnumerator CountDown()
        {
            float countdown = timer;

            while (countdown > 0)
            {
                int seconds = Mathf.CeilToInt(countdown);
                if (isTextAnimatable)
                {
                    StartCoroutine(AnimateText(seconds));
                }
                else
                {
                    this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1);
                    this.countDownText.fontSize = 50;
                    this.countDownText.text = seconds.ToString();

                }
                yield return new WaitForSeconds(interval);
                countdown -= interval;
            }
            this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 0);
            this.gameManager.SetState(stateToSet);
        }
        public IEnumerator AnimateText(int num)
        {
            this.countDownText.text = num.ToString();
            float duration = 1f;
            float halfDuration = duration / 2;

            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, progress);
                this.countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                yield return null;
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1 - progress);
                yield return null;
            }
        }

        public IEnumerator AnimateText(string text)
        {
            this.countDownText.text = text;
            float duration = 1f;
            float halfDuration = duration / 2;

            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, progress);
                this.countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                yield return null;
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1 - progress);
                yield return null;
            }
        }

        private void HandleStartEvent()
        {
            countDownText.enabled = true;
            StartCoroutine(CountDown());
        }
    }

    
}
