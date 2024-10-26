using Zenject;

namespace UpgradesManager
{
    public sealed class UpgradeUnloadStorage : Upgrade
    {
        private ConveyorEntity conveyorEntity;
        private readonly UnloadStorageUpgradeConfig _config;
        public int NextLevelCapacity
        {
            get => _config.capacityStep * CurrentLevel;
        }
        public UpgradeUnloadStorage(UnloadStorageUpgradeConfig upgradeConfig) : base(upgradeConfig)
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
            conveyorEntity.Get<IConveyor_SetUnloadStorageComponent>().
                SetUnloadStorage(NextLevelCapacity);
        }
    }
    
}