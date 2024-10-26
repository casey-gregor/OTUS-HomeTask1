using Game.GamePlay.Upgrades;

namespace UpgradesManager
{
    public sealed class LoadStoragePresenter : Presenter
    {
        private readonly View _view;
        private readonly UpgradeLoadStorage _upgrade;
        private readonly IMoneyStorage _moneyStorage;

        public LoadStoragePresenter(
            View view, 
            Upgrade loadStorageUpgrade, 
            IMoneyStorage moneyStorage)
        :base(view, moneyStorage, loadStorageUpgrade)
        {
            _view = view;
            _moneyStorage = moneyStorage;
            _upgrade = loadStorageUpgrade as UpgradeLoadStorage;
        }

        protected override void HandleUpdateButtonClick()
        {
            _moneyStorage.SpendMoney(_upgrade.NextLevelPrice);
            _upgrade.LevelUp();
            UpdateViewData();
        }
    
        public override void UpdateViewData()
        {
            _view.UpdateCapacity(_upgrade.NextLevelCapacity);
            _view.UpdateLevel(_upgrade.CurrentLevel, _upgrade.MaxLevel);
            _view.UpdateNextPrice(_upgrade.NextLevelPrice);
            CheckIfCanBuy(_upgrade.NextLevelPrice, _upgrade.IsMaxLevel);
        }
        
    }
}