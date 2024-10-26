using UnityEngine;

namespace UpgradesManager
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public int maxLevel;
        public int basePrice;
        
        public abstract Upgrade CreateUpgrade();
    }
}