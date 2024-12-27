using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
    [RequireComponent(typeof(Image))]
    public class ItemView : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void ToggleQtyPopup(bool value)
        {
            text.transform.parent.gameObject.SetActive(value);
        }

        public void UpdateQtyPopup(int value)
        {
            text.text = value.ToString();
        }
    }
}