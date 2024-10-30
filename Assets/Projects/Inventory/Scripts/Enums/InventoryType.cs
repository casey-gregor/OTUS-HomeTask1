using System;

namespace Inventory
{
    [Flags]
    public enum InventoryType
    {
        None = 0,
        Consumable = 1,
        Wearable = 2,
        Stackable = 4
    }
}