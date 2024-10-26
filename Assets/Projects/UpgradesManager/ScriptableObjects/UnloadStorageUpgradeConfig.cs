using UnityEngine;

namespace UpgradesManager
{
    [CreateAssetMenu(
        fileName = "NewUnloadCapacityUpgradeConfig", 
        menuName = "UpgradesManager/New UnloadCapacityUpgradeConfig", order = 0)]
    public class UnloadStorageUpgradeConfig : UpgradeConfig
    {
        public int capacityStep;
        public override Upgrade CreateUpgrade()
        {
            return new UpgradeUnloadStorage(this);
        }
    }
}