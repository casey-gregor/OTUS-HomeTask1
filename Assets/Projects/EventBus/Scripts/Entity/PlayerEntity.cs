using System.Collections.Generic;
using UI;

namespace EventBus
{
    public sealed class PlayerEntity
    {
        public readonly int PlayerId;
        public readonly HeroComponent HeroComponent;
        public PlayerEntity(
            int playerId,
            HeroListView playerView, 
            IReadOnlyList<HeroView> heroViews, 
            HeroEntitiesCollection heroEntities)
        {
            PlayerId = playerId;
            HeroComponent = new HeroComponent(playerView, heroViews, heroEntities);
        }
    }
}