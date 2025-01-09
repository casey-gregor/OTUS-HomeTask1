using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    public sealed class ChestPresenter : IDisposable
    {
        public string ChestId => _chestModel.ChestId;
        public bool IsUnlocked => _chestModel.IsUnlocked;
        public DateTime TimeToOpen => _chestModel.TimeToOpen;
        public TimeSpan InitialTimer => _chestModel.InitialTimer;
        public Button OpenButton => _chestView.OpenButton;
        public IReadOnlyList<IReward> Rewards => _chestModel.Rewards;
        public Image ImageComponent => _chestView.ImageComponent;
        public Transform ViewTransform => _chestView.transform;
        public ParticleSystem OpenEffect => _chestView.OpenEffect;
        
        private readonly ChestView _chestView;
        private readonly ChestModel _chestModel;
        
        public ChestPresenter(ChestModel chestModel, ChestView chestView)
        {
            _chestModel = chestModel;
            _chestView = chestView;
        }
        
        public bool HasChestView(ChestView chestView)
        {
            return _chestView == chestView;
        }

        public void OpenChest()
        {
            _chestView.SetChestSpriteImage(true);
        }
        
        public void CloseChest()
        {
            _chestView.SetChestSpriteImage(false);
        }

        public void InitializeChestData(string chestId, List<IReward> rewards)
        {
            _chestModel.SetData(chestId, rewards);
            SetViewPanelTitle(chestId);
        }

        public void SetIsUnlocked(bool value)
        {
            _chestModel.SetIsUnlocked(value);
            _chestView.SetIsUnlocked(value);
        }
        
        public void SetViewPanelTitle(string title)
        {
            _chestView.SetChestPanelTitle(title);
        }

        public void UpdateChestTimer(TimeSpan time)
        {
            _chestView.UpdateChestTimer(time);
        }

        public void SetOpenTime(DateTime time)
        {
            _chestModel.SetOpenTime(time);
        }
        
        public void SetInitialTimer(TimeSpan fromMinutes)
        {
            _chestModel.SetInitialTimer(fromMinutes);
        }

        public void Dispose()
        {
            GameObject.Destroy(_chestView.gameObject);
        }
    }
}