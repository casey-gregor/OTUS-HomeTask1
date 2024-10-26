

namespace UpgradesManager
{
    public abstract class Upgrade
    {
        private readonly UpgradeConfig _upgradeConfig;
        public int CurrentLevel { get => _level; }
        public int MaxLevel { get => _upgradeConfig.maxLevel; }
        public bool IsMaxLevel
        {
            get => CurrentLevel >= MaxLevel;
        }
        public int NextLevelPrice
        {
            get
            {
                if (IsMaxLevel)
                {
                    return -1;
                }
                
                return _upgradeConfig.basePrice * (_level + 1);
            }
        } 

        private int _level;

        protected Upgrade(UpgradeConfig upgradeConfig)
        {
            _upgradeConfig = upgradeConfig;
            _level = 1;
        }
        public void LevelUp()
        {
            if (_level < MaxLevel)
            {
                _level++;
                OnLevelUpgrade();
            }
        }
        protected abstract void OnLevelUpgrade();
    }
}