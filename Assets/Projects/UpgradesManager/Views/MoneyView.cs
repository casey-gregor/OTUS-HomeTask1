using TMPro;
using UnityEngine;

namespace UpgradesManager
{
    public class MoneyView : MonoBehaviour
    {
        private TextMeshProUGUI _moneyTextComponent;
        
        private void Awake()
        {
            _moneyTextComponent = GetComponent<TextMeshProUGUI>();
        }
        
        public void UpdateMoneyView(int value)
        {
            _moneyTextComponent.text = $"{value}";
        }
    }
}