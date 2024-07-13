using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class TextCountdownComponent : IDisposable
    { 

        private TextCountdownConfig config;
        private StartButton startButton;
        private GameManager gameManager;
        private TextMeshProUGUI countDownText;

        public TextCountdownComponent
            (
            TextCountdownConfig config,
            StartButton startButton,
            GameManager gameManager,
            TextMeshProUGUI textMeshPro
            )
        {
            this.config = config;
            this.startButton = startButton;
            this.gameManager = gameManager;
            this.countDownText = textMeshPro;


            this.countDownText.fontSize = 20;
            this.countDownText.enabled = false;
            startButton.StartEvent += HandleStartEvent;
        }

        private void HandleStartEvent()
        {
            
            this.countDownText.enabled = true;
            CountDown().Forget();
        }

        public async UniTaskVoid CountDown()
        {
            float countdown = this.config.timer;

            while (countdown > 0)
            {
                int seconds = Mathf.CeilToInt(countdown);
                if (this.config.isTextAnimatable)
                {
                    await AnimateTexts(seconds);
                }
                else
                {
                    this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1);
                    this.countDownText.fontSize = 50;
                    this.countDownText.text = seconds.ToString();

                }
                countdown -= this.config.interval;
            }
            this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 0);
            this.gameManager.SetState(this.config.stateToSet);
        }
        public async UniTask AnimateTexts(int num)
        {
            this.countDownText.text = num.ToString();
            float duration = 1f;
            float halfDuration = duration / 2;

            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float progress = t / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, progress);
                this.countDownText.fontSize = Mathf.Lerp(20, 80, progress);
                await UniTask.Yield();
            }

            for (float t = halfDuration; t < duration; t += Time.deltaTime)
            {
                float progress = (t - halfDuration) / halfDuration;
                this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1 - progress);
                await UniTask.Yield();
            }
        }

        public void Dispose()
        {
            startButton.StartEvent -= HandleStartEvent;
        }

        //public IEnumerator AnimateText(string text)
        //{
        //    this.countDownText.text = text;
        //    float duration = 1f;
        //    float halfDuration = duration / 2;

        //    for (float t = 0; t < halfDuration; t += Time.deltaTime)
        //    {
        //        float progress = t / halfDuration;
        //        this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, progress);
        //        this.countDownText.fontSize = Mathf.Lerp(20, 80, progress);
        //        yield return null;
        //    }

        //    for (float t = halfDuration; t < duration; t += Time.deltaTime)
        //    {
        //        float progress = (t - halfDuration) / halfDuration;
        //        this.countDownText.color = new Color(this.countDownText.color.r, this.countDownText.color.g, this.countDownText.color.b, 1 - progress);
        //        yield return null;
        //    }
        //}
    }


}
