
namespace Inventory
{
    public sealed class Inventory
    {
        public EventNotifier EventNotifier { get; private set; }
        public SlotsManager SlotsManager { get; private set; }

        public Inventory(
            EventNotifier eventNotifier,
            SlotsManager slotsManager)
        {
            EventNotifier = eventNotifier;
            SlotsManager = slotsManager;
        }

        public bool ConsumeItemFromBackpack(InventoryItem item)
        {
            if (SlotsManager.CheckIfConsumable(item))
            {
                SlotsManager.TryRemoveItem(item);
                EventNotifier.NotifyItemConsumed(item);
                EventNotifier.NotifySlotUpdated(SlotsManager.GetSlot(SlotType.Backpack));
                return true;
            }
            EventNotifier.NotifyItemConsumedFailed(item.name);
            return false;
        }
        public void AddItem(InventoryItem item)
        {
            if (item.inventoryType == InventoryType.None)
            {
                EventNotifier.NotifyInventoryTypeNone();
            }

            if(!TryAddItem(item))
                EventNotifier.NotifyItemAddedFailed(item.name);
        }
        
        public void RemoveItem(InventoryItem item)
        {
            var slot = SlotsManager.TryRemoveItem(item);
            if (slot != null)
            {
                if (item.inventoryType.HasFlag(InventoryType.Wearable) 
                    && slot.GetSlotType() != SlotType.Backpack)
                {
                    EventNotifier.NotifyWearableRemoved(item);
                }
                EventNotifier.NotifyItemRemoved(item, slot);
                EventNotifier.NotifySlotUpdated(slot);
            }
            else
            {
                EventNotifier.NotifyItemRemovedFailed(item.name);
            }
        }
        
        private bool TryAddItem(InventoryItem item)
        {
            var slot = SlotsManager.TryAddItem(item);
            if (slot != null)
            {
                if (slot != SlotsManager.GetSlot(SlotType.Backpack) &&
                    item.inventoryType.HasFlag(InventoryType.Wearable))
                {
                    EventNotifier.NotifyWearableAdded(item);
                }
                EventNotifier.NotifyItemAdded(item, slot);
                EventNotifier.NotifySlotUpdated(slot);
                return true;
            }

            return false;
        }
    }
}