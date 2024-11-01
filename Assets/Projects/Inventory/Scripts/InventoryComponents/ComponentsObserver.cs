
namespace Inventory
{
    public sealed class ComponentsObserver
    {
        private readonly InventoryManager _inventoryManager;
        private readonly Entity _entity;

        public ComponentsObserver(InventoryManager inventoryManager, Entity entity)
        {
            _inventoryManager = inventoryManager;
            _inventoryManager.OnInventoryInitialized += SubscribeToInventory;
            _entity = entity;
        }

        private void SubscribeToInventory()
        {
            var notifier = _inventoryManager.Inventory.EventNotifier;
            notifier.OnItemConsumed += HandleWearableAdded;
            notifier.OnWearableAdded += HandleWearableAdded;
            notifier.OnWearableRemoved += HandleWearableRemoved;
        }
        

        private void HandleWearableRemoved(InventoryItem item)
        {
            foreach (IItemComponent component in item.itemComponents)
            {
                component.Remove(_entity);
            }
        }
        
        private void HandleWearableAdded(InventoryItem item)
        {
            foreach (IItemComponent component in item.itemComponents)
            {
                component.Apply(_entity);
            }
        }
    }
}