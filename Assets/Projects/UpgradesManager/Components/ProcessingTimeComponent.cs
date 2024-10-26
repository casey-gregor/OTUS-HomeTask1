using System;
using Game.GamePlay.Upgrades;
using UnityEngine;
using Zenject;

namespace UpgradesManager
{
    [Serializable]
    public sealed class ProcessingTimeComponent
    {
        public ProcessingTimeUpgradeConfig config;
        
        public Upgrade Upgrade {get; private set;}
        public ProcessingTimePresenter Presenter {get; private set;}

        public void InitializeComponent(
            DiContainer container, 
            UIComponent uiComponent, 
            MoneyStorage moneyStorage)
        {
            if (config != null)
            {
                Initiate(container, uiComponent, moneyStorage);
            }
            else
            {
                Debug.LogWarning("No ProcessingTimeUpgradeConfig found");
            }
        }

        private void Initiate(DiContainer container, UIComponent uiComponent, MoneyStorage moneyStorage)
        {
            InitiateUpgrade(container);
            InitiatePresenter(uiComponent, moneyStorage);
        }
        
        private void InitiatePresenter(UIComponent uiComponent, MoneyStorage moneyStorage)
        {
            Presenter = new ProcessingTimePresenter(
                uiComponent.processingTimeView,
                Upgrade,
                moneyStorage);
                
            Presenter.SubscribeToUpgradeButton();
            Presenter.SubscribeToMoneyChange();

            Presenter.UpdateViewData();
        }

        private void InitiateUpgrade(DiContainer container)
        {
            Upgrade = config.CreateUpgrade();
            container.Inject(Upgrade);
        }
    }
}