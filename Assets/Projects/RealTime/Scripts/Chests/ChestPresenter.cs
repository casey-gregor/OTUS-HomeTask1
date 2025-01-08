using System;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    public class ChestPresenter : IDisposable
    {
        private readonly ChestView _chestView;
        
        public ChestPresenter(ChestView chestView)
        {
            _chestView = chestView;
        }

        public ChestView GetChestView()
        {
            return _chestView;
        }
        // public Button GetOpenButton()
        // {
        //     return _chestView.OpenButton;
        // }
        
        public void SetViewPanelTitle(string title)
        {
            _chestView.SetChestPanelTitle(title);
        }
        
        public void ToggleOpenButton(bool value)
        {
            _chestView.OpenButton.gameObject.SetActive(value);
        }

        public void ToggleTimerVisibility(bool value)
        {
            _chestView.Timer.transform.parent.gameObject.SetActive(value);
        }
        
        public void OpenChest()
        {
            _chestView.SetChestSpriteImage(true);
        }

        public void CloseChest()
        {
            _chestView.SetChestSpriteImage(false);
        }

        public void UpdateChestTimer(TimeSpan time)
        {
            _chestView.UpdateChestTimer(time);
        }

        public void Dispose()
        {
            GameObject.Destroy(_chestView.gameObject);
        }
    }
}