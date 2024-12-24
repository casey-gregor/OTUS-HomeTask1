using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemInstantiator
    {
        public void CreateItem(ItemView prefab, InventoryItem item, Transform parent)
        {
            var obj = GameObject.Instantiate(prefab, parent);
            obj.GetComponent<Image>().sprite = item.icon;
            obj.name = item.name;
        }
    }
}