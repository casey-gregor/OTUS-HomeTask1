using System.Collections.Generic;
using UI;

namespace EventBus
{
    public sealed class HeroComponent
    {
        public readonly HeroListView PlayerView;
        public readonly IReadOnlyList<HeroView> HeroViews;
        public readonly HeroEntitiesCollection HeroEntities;
        public int LastAttackHeroIndex;
        public HeroComponent(
            HeroListView playerView, 
            IReadOnlyList<HeroView> heroViews, 
            HeroEntitiesCollection heroEntities)
        {
            PlayerView = playerView;
            HeroViews = heroViews;
            HeroEntities = heroEntities;
            LastAttackHeroIndex = 0;
        }
        
        public int GetAliveHeroesCount()
        {
            return HeroEntities.GetAliveHeroesCount();
        }
        
        public int GetAllHeroesCount()
        {
            return HeroEntities.GetAllHeroesCount();
        }
        
        public HeroEntity GetRandomHero()
        {
            if (GetAliveHeroesCount() == 0)
            {
                return null;
            }

            while (true)
            {
                int numOfHeroes = HeroViews.Count;
                int randomIndex = UnityEngine.Random.Range(0, numOfHeroes);
        
                if (HeroEntities.TryGetHeroEntity(randomIndex, out HeroEntity heroEntity) && 
                    !heroEntity.HealthComponent.IsDead)
                {
                    return heroEntity;
                }
            }
        }
    }
}