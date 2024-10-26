using UnityEngine;
using UnityEngine.Serialization;

namespace UpgradesManager
{
    [CreateAssetMenu(
        fileName = "NewLoadCapacityUpgradeConfig", 
        menuName = "UpgradesManager/New LoadCapacityUpgradeConfig", order = 0)]
    public class LoadCapacityUpgradeConfig : UpgradeConfig
    {
        public int capacityStep;
        public override Upgrade CreateUpgrade()
        {
            return new UpgradeLoadStorage(this);
        }
    }
}