using Zenject;

namespace UpgradesManager
{
    public sealed class UpgradeLoadStorage : Upgrade
    {
        private ConveyorEntity conveyorEntity;
        private readonly LoadCapacityUpgradeConfig _config;
        public int NextLevelCapacity
        {
            get => _config.capacityStep * CurrentLevel;
        }
        public UpgradeLoadStorage(
            LoadCapacityUpgradeConfig upgradeConfig) : 
            base(upgradeConfig)
        {
            _config = upgradeConfig;
        }

        [Inject]
        private void Construct(Conveyor conveyor)
        {
            conveyorEntity = conveyor.conveyorEntity;
        }

        protected override void OnLevelUpgrade()
        {
            conveyorEntity.Get<IConveyor_SetLoadStorageComponent>().
                SetLoadStorage(NextLevelCapacity);
        }
    }
}