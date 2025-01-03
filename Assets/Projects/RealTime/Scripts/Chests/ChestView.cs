using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI panelTitle;
        [SerializeField] private Image chestImage;
        [SerializeField] private Sprite openSprite;
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private Button openButton;
        [SerializeField] private TextMeshProUGUI timer;

        public event Action OnChestOpened;
        public Button OpenButton => openButton;

        public Image ImageComponent => chestImage;

        public void SetChestPanelTitle(string value)
        {
            panelTitle.text = value;
        }

        public void SetChestSpriteImage(bool value)
        {
            if (value)
            {
                ImageComponent.sprite = openSprite;
            }
            else
            {
                ImageComponent.sprite = closedSprite;
            }
        }

        public void UpdateChestTimer(TimeSpan time)
        {
            timer.text = $"Open timer : \n{time.Minutes:D2}:{time.Seconds:D2}";
        }

        public void OpenChest()
        {
            OnChestOpened?.Invoke();
        }
        
    }
}