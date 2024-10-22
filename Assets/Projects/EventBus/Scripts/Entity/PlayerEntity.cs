using System.Collections.Generic;
using UI;

namespace EventBus
{
    public class PlayerEntity
    {
        public int PlayerId { get; private set; }
        public HeroListView PlayerView;
        public readonly IReadOnlyList<HeroView> HeroViews;
        public readonly HeroEntitiesCollection HeroEntities;

        public int LastAttackHeroIndex;
        public Ability Ability;
        public PlayerEntity(
            int playerId,
            HeroListView playerView, 
            IReadOnlyList<HeroView> heroViews, 
            HeroEntitiesCollection heroEntities)
        {
            PlayerId = playerId;
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
        
                if (HeroEntities.TryGetHeroEntity(randomIndex, out HeroEntity heroEntity) && !heroEntity.IsDead)
                {
                    return heroEntity;
                }
            }
        }
        
        
        
    }
}