using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    public sealed class Chest : MonoBehaviour
    {
        public Sprite openSprite;
        public Sprite closedSprite;
        public Button openButton;
        public string ChestId { get; private set; }
        [ShowInInspector] public string timeToOpen => TextFormatter.DateTimeToString(TimeToOpen);
        [ShowInInspector] public string receivedTime => TextFormatter.DateTimeToString(ReceivedTime);
        public List<IReward> Rewards { get; private set; }
        public DateTime TimeToOpen { get; private set; }
        public DateTime ReceivedTime { get; private set; }
        public TimeSpan TimerMinutes {get; private set;}
        public bool IsUnlocked { get; private set; }
        public Image ImageComponent
        {
            get
            {
                if (_imageComponent == null)
                {
                    _imageComponent = GetComponentInChildren<Image>();
                    if(_imageComponent == null)
                        Debug.LogError($"No Image component found in {gameObject.name}");
                }
                return _imageComponent;
                
            }
        }
        private Image _imageComponent;

        public void SetId(string chestId)
        {
            ChestId = chestId;
        }

        public void SetBonuses(List<IReward> bonuses)
        {
            Rewards = bonuses;
        }
        public void SetIsUnlocked(bool value)
        {
            IsUnlocked = value;
            if (IsUnlocked)
            {
                openButton.gameObject.SetActive(true);
            }
            else
            {
                openButton.gameObject.SetActive(false);
            }
        }

        public void SetReceivedTime(DateTime value)
        {
            ReceivedTime = value;
        }

        public void SetOpenTime(DateTime value)
        {
            TimeToOpen = value;
        }
        public void OpenChest()
        {
            ImageComponent.sprite = openSprite;
        }
        
        public void CloseChest()
        {
            ImageComponent.sprite = closedSprite;
        }

        public void SetTimerMinutes(TimeSpan value)
        {
            TimerMinutes = value;
        }
    }
}