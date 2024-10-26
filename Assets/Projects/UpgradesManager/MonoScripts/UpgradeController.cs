using Game.GamePlay.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace UpgradesManager
{
    public sealed class UpgradeController : MonoBehaviour
    {
        public UIComponent uiComponent;
        public MoneyStorage moneyStorage;
        public int startMoney = 0;
        
        [SerializeField] private LoadStorageComponent loadStorageComponent;
        [SerializeField] private UnloadStorageComponent unloadStorageComponent;
        [SerializeField] private ProcessingTimeComponent processingTimeComponent;
        
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        private void Awake()
        {
            moneyStorage.OnMoneyChanged += HandleMoneyChange;
            uiComponent.moneyView.UpdateMoneyView(startMoney);
            
            loadStorageComponent.InitializeComponent(_container, uiComponent, moneyStorage);
            unloadStorageComponent.InitializeComponent(_container, uiComponent, moneyStorage);
            processingTimeComponent.InitializeComponent(_container, uiComponent, moneyStorage);
        }

        private void OnDestroy()
        {
            UnsubscribeComponent(loadStorageComponent.Presenter);
            UnsubscribeComponent(unloadStorageComponent.Presenter);
            UnsubscribeComponent(processingTimeComponent.Presenter);
            

            if (moneyStorage != null)
            {
                moneyStorage.OnMoneyChanged -= HandleMoneyChange;
            }
        }

        private void UnsubscribeComponent(Presenter presenter)
        {
            if (presenter != null)
            {
                presenter.UnsubscribeFromUpgradeButton();
                presenter.UnsubscribeToMoneyChange();
            }
        }

        private void HandleMoneyChange(int value)
        {
            uiComponent.moneyView.UpdateMoneyView(value);
        }
        
        [Button]
        private void AddMoney(int value)
        {
            moneyStorage.EarnMoney(value);
        }
    }
}