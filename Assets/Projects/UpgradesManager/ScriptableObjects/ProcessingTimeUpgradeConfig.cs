using UnityEngine;

namespace UpgradesManager
{
    [CreateAssetMenu(
        fileName = "NewProcessingTimeUpgradeConfig", 
        menuName = "UpgradesManager/New ProcessingTimeUpgradeConfig", order = 0)]
    public class ProcessingTimeUpgradeConfig : UpgradeConfig
    {
        public int capacityStep;
        public override Upgrade CreateUpgrade()
        {
            return new UpgradeProcessingTime(this);
        }
    }
}