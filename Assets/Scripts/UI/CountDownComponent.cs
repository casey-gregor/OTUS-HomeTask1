using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class CountDownComponent
    {
        private GameManager _gameManager;
        private MonoBehaviour _controlClass;
        private TextMeshProUGUI _countDownText;

        public CountDownComponent(GameManager gameManager, MonoBehaviour controlClass, TextMeshProUGUI counDownText)
        {
            this._gameManager = gameManager;
            this._controlClass = controlClass;
            this._countDownText = counDownText;
        }

        public IEnumerator CountDown(float timer, float interval, GameManager.State stateToSet, bool isTextAnimatable)
        {
            float countdown = timer;

            while (countdown > 0)
            {
                int seconds = Mathf.CeilToInt(countdown);
                if (isTextAnimatable)
                {
                    this._controlClass.StartCoroutine(AnimateText(seconds));
                }
                else
                {
                    this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, 1);
                    this._countDownText.fontSize = 50;
                    this._countDownText.text = seconds.ToString();

                }
                yield return new WaitForSeconds(interval);
                countdown -= interval;
            }
            this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, 0);
            this._gameManager.SetState(stateToSet);
        }
        public IEnumerator AnimateText(int num)
        {
            this._countDownText.text = num.ToString();
            float duration = 1f;
            float halfDuration = duration / 2;

            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t / halfDuration;
                this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, progress);
                this._countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                yield return null;
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, 1 - progress);
                yield return null;
            }
        }

        public IEnumerator AnimateText(string text)
        {
            this._countDownText.text = text;
            float duration = 1f;
            float halfDuration = duration / 2;

            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t / halfDuration;
                this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, progress);
                this._countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                yield return null;
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                this._countDownText.color = new Color(this._countDownText.color.r, this._countDownText.color.g, this._countDownText.color.b, 1 - progress);
                yield return null;
            }
        }
    }

    
}
