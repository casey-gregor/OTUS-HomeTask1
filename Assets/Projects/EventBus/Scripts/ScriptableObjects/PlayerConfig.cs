using UnityEngine;

namespace EventBus
{
    [CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "EventBusConfigs/New PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public HeroConfig[] heroes;
    }
}