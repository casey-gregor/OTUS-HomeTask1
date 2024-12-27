namespace Inventory
{
    public interface IInventoryHandler
    {
        bool CanHandle(InventoryItem item);
        void HandleAdd(InventoryItem item);
        void HandleRemove(InventoryItem item);
    }
}