using TMPro;
using UnityEngine;

namespace UpgradesManager
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI capacity;
        [SerializeField] protected TextMeshProUGUI level;
        [SerializeField] protected TextMeshProUGUI nextPrice;

        public abstract void UpdateCapacity(int value);

        public void UpdateLevel(int currentLevel, int maxLevel)
        {
            string levelText = $"Level : {currentLevel}/{maxLevel}";
            level.text = levelText;
        }

        public void UpdateNextPrice(int value)
        {
            string nextPriceText = $"Upgrade for\n${value}";
            if (value == -1)
                nextPriceText = "MaxLevel";
            nextPrice.text = nextPriceText;
        }
        
    }
}