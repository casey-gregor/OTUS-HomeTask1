using UnityEngine;

namespace EventBus
{
    [CreateAssetMenu(fileName = "NewAllHeroesConfig", menuName = "EventBusConfigs/New AllHeroesConfig", order = 0)]
    public class AllHeroesConfig : ScriptableObject
    {
        public HeroConfig[] heroes;
    }
}