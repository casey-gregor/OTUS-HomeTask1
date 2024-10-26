using Game.GamePlay.Upgrades;

namespace UpgradesManager
{
    public sealed  class UnloadStoragePresenter : Presenter
    {
        private readonly View _view;
        private readonly UpgradeUnloadStorage _upgrade;
        private readonly IMoneyStorage _moneyStorage;
        
        public UnloadStoragePresenter(
            View view, 
            Upgrade unloadStorageUpgrade, 
            IMoneyStorage moneyStorage) : 
            base(view, moneyStorage, unloadStorageUpgrade)
        {
            _view = view;
            _upgrade = unloadStorageUpgrade as UpgradeUnloadStorage;
            _moneyStorage = moneyStorage;
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