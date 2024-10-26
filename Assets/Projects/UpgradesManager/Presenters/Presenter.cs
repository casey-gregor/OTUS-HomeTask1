using Game.GamePlay.Upgrades;
using UnityEngine.UI;

namespace UpgradesManager
{
    public abstract class Presenter
    {
        protected View View;
        protected IMoneyStorage MoneyStorage;
        protected Upgrade Upgrade;

        protected Presenter(View view, IMoneyStorage moneyStorage, Upgrade upgrade)
        {
            View = view;
            MoneyStorage = moneyStorage;
            Upgrade = upgrade;
        }
        
        public void SubscribeToUpgradeButton()
        {
            Button button = View.GetComponentInChildren<Button>();;
            button.onClick.AddListener(HandleUpdateButtonClick);
        }
        
        public void UnsubscribeFromUpgradeButton()
        {
            if (View != null)
            {
                Button button = View.GetComponentInChildren<Button>();;
                button.onClick.RemoveAllListeners();
            }
        }
        protected void CheckIfCanBuy(int value, bool isMaxLevel)
        {
            Button button = View.GetComponentInChildren<Button>();
            
            if (MoneyStorage.CanSpendMoney(value) && !isMaxLevel)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        
        public void SubscribeToMoneyChange()
        {
            MoneyStorage.OnMoneyChanged += HandleMoneyChanged;
        }
        
        public void UnsubscribeToMoneyChange()
        {
            MoneyStorage.OnMoneyChanged -= HandleMoneyChanged;
        }

        protected void HandleMoneyChanged(int _)
        {
            CheckIfCanBuy(Upgrade.NextLevelPrice, Upgrade.IsMaxLevel);
        }

        protected abstract void HandleUpdateButtonClick();

        public abstract void UpdateViewData();
        
    }
}