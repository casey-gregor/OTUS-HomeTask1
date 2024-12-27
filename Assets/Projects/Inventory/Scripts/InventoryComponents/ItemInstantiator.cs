using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemInstantiator
    {
        public void CreateItem(ItemView prefab, InventoryItem item, Transform inventorySlot)
        {
            var obj = GameObject.Instantiate(prefab, inventorySlot);
            obj.GetComponent<Image>().sprite = item.icon;
            obj.name = item.name;
            item.AddItemView(obj);
        }
    }
}