using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace RealTime.Rewards
{
    public class RewardsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI resourcesText;
        private void Awake()
        {
            transform.localScale = Vector3.zero;
            transform.localPosition = Vector3.zero;
            ToggleParent(moneyText, false);
            ToggleParent(resourcesText, false);
            
        }

        public void SetMoneyReward(string moneyAmount)
        {
            ToggleParent(moneyText, true);
            moneyText.text = $" : {moneyAmount}";;
        }

        public void SetResourcesReward(string resourcesAmount)
        {
            ToggleParent(resourcesText, true);
            resourcesText.text = $" : {resourcesAmount}";
        }

        private void ToggleParent(TextMeshProUGUI textObj, bool value)
        {
            textObj.transform.parent.gameObject.SetActive(value);
        }
        
    }
}