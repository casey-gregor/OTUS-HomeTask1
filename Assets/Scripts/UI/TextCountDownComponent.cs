using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class TextCountDownComponent
    {
        private GameManager gameManager;
        private MonoBehaviour controlClass;
        private TextMeshProUGUI countDownText;

        public TextCountDownComponent(GameManager gameManager, MonoBehaviour controlClass, TextMeshProUGUI counDownText)
        {
            this.gameManager = gameManager;
            this.controlClass = controlClass;
            this.countDownText = counDownText;
        }

        public IEnumerator CountDown(float timer, float interval, GameManager.State stateToSet, bool isTextAnimatable)
        {
            float countdown = timer;

            while (countdown > 0)
            {
                int seconds = Mathf.CeilToInt(countdown);
                if (isTextAnimatable)
                {
                    this.controlClass.StartCoroutine(AnimateText(seconds));
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
    }

    
}
