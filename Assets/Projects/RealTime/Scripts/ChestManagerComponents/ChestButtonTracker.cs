using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestButtonTracker
    {
        public event Action<ChestModel> OnChestButtonPressed;
        private readonly Dictionary<ChestView, ChestModel> _chests = new();

        public void AddToList(ChestModel chestModel)
        {
            _chests.Add(chestModel.GetChestView(), chestModel);
            SubscribeToChest(chestModel.GetChestView());
        }

        private void RemoveFromList(ChestView chestView)
        {
            _chests.Remove(chestView);
            UnsubscribeFromChest(chestView);
        }

        private void UnsubscribeFromChest(ChestView chestView)
        {
            ButtonClickProcessor button = chestView.OpenButton.GetComponent<ButtonClickProcessor>();
            if (button != null)
            {
                button.OnButtonClick -= HandleOpenButton;
            }
        }

        private void SubscribeToChest(ChestView chestView)
        {
            
            ButtonClickProcessor button = chestView.OpenButton.GetComponent<ButtonClickProcessor>();
            if (button != null)
            {
                button.OnButtonClick += HandleOpenButton;
            }
        }

        public void HandleOpenButton(ChestView chestView)
        {
            ChestModel chestModel = _chests[chestView];
            OnChestButtonPressed?.Invoke(chestModel);
            RemoveFromList(chestView);
        }
    }
}