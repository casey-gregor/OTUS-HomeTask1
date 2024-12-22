using Zenject;

namespace UpgradesManager
{
    public sealed class UpgradeProcessingTime : Upgrade
    {
        private ConveyorEntity conveyorEntity;
        private readonly ProcessingTimeUpgradeConfig _config;
        public int NextLevelCapacity
        {
            get => _config.capacityStep * CurrentLevel;
        }
        public UpgradeProcessingTime(
            ProcessingTimeUpgradeConfig upgradeConfig) : 
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
            conveyorEntity.Get<IConveyor_SetProduceTimeComponent>().
                SetProduceTime(NextLevelCapacity);
        }
    }
}