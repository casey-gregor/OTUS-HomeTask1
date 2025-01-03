using System;
using System.Collections.Generic;
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
            _chestView.OnChestOpened += OpenChest;
        }

        public ChestView GetChestView()
        {
            return _chestView;
        }
        public Button GetOpenButton()
        {
            return _chestView.OpenButton;
        }
        
        public void SetViewPanelTitle(string title)
        {
            _chestView.SetChestPanelTitle(title);
        }
        
        public void ToggleOpenButton(bool value)
        {
            _chestView.OpenButton.gameObject.SetActive(value);
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
            _chestView.OnChestOpened -= OpenChest;
            GameObject.Destroy(_chestView.gameObject);
        }
    }
}