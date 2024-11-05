using System;
using System.Collections.Generic;

namespace RealTime
{
    public sealed class ChestButtonTracker
    {
        public event Action<Chest> OnChestButtonPressed;
        private readonly List<Chest> _chests = new();

        public void AddToList(Chest chest)
        {
            _chests.Add(chest);
            SubscribeToChest(chest);
        }

        public void RemoveFromList(Chest chest)
        {
            _chests.Remove(chest);
            UnsubscribeFromChest(chest);
        }

        private void UnsubscribeFromChest(Chest chest)
        {
            ButtonClickProcessor button = chest.openButton.GetComponent<ButtonClickProcessor>();
            if(button != null)
                button.OnButtonClick -= HandleOpenButton;
        }

        private void SubscribeToChest(Chest chest)
        {
            ButtonClickProcessor button = chest.openButton.GetComponent<ButtonClickProcessor>();
            if(button != null)
                button.OnButtonClick += HandleOpenButton;
        }

        public void HandleOpenButton(Chest chest)
        {
            OnChestButtonPressed?.Invoke(chest);
        }
    }
}