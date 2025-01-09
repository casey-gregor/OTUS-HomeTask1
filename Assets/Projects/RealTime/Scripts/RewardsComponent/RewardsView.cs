using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime.Rewards
{
    public class RewardsView : MonoBehaviour
    {
        public event Action<RewardsView> OnCloseButtonClicked;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI resourcesText;
        [SerializeField] private Button closeButton;
        
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
        
        private void Awake()
        {
            closeButton.onClick.AddListener(HandleCloseButtonClicked);
            transform.localScale = Vector3.zero;
            transform.localPosition = Vector3.zero;
            ToggleParent(moneyText, false);
            ToggleParent(resourcesText, false);
            
        }

        private void HandleCloseButtonClicked()
        {
            OnCloseButtonClicked?.Invoke(this);
        }

        private void ToggleParent(TextMeshProUGUI textObj, bool value)
        {
            textObj.transform.parent.gameObject.SetActive(value);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(HandleCloseButtonClicked);
        }
    }
}