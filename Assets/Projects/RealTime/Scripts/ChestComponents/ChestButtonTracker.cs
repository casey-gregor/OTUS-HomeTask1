using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestButtonTracker
    {
        public event Action<ChestPresenter> OnChestButtonPressed;
        private readonly List< ChestPresenter> _chests = new();

        public void AddToList(ChestPresenter chestPresenter)
        {
            _chests.Add(chestPresenter);
            SubscribeToChest(chestPresenter);
        }

        private void RemoveFromList(ChestPresenter chestPresenter)
        {
            _chests.Remove(chestPresenter);
            UnsubscribeFromChest(chestPresenter);
        }

        private void UnsubscribeFromChest(ChestPresenter chestPresenter)
        {
            ButtonClickProcessor button = chestPresenter.OpenButton.GetComponent<ButtonClickProcessor>();
            if (button != null)
            {
                button.OnButtonClick -= HandleOpenButton;
            }
        }

        private void SubscribeToChest(ChestPresenter chestPresenter)
        {
            
            ButtonClickProcessor button = chestPresenter.OpenButton.GetComponent<ButtonClickProcessor>();
            if (button != null)
            {
                button.OnButtonClick += HandleOpenButton;
            }
        }

        private void HandleOpenButton(ChestView chestView)
        {
            for (var index = 0; index < _chests.Count; index++)
            {
                var chestPresenter = _chests[index];
                if (chestPresenter.HasChestView(chestView))
                {
                    OnChestButtonPressed?.Invoke(chestPresenter);
                    RemoveFromList(chestPresenter);
                }
            }
        }
    }
}