using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewItemConfig", menuName = "Inventory/New Item Config", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        public InventoryItem inventoryItem;
    }
}