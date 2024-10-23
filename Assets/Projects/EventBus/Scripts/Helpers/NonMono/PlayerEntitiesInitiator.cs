using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class PlayerEntitiesInitiator
    {
        private readonly UIService _uiService;
        private readonly Dictionary<int, PlayerEntity> _playerEntityDict = new();

        public PlayerEntitiesInitiator(UIService uiService)
        {
            _uiService = uiService;
        }
        
        public void CreatePlayerEntities(AllHeroesConfig config)
        {
            HeroListView[] playersArray = _uiService.GetPlayers();
            
            List<HeroConfig> heroes = config.heroes.ToList();
            int numberOfHeroesPerPlayer = heroes.Count / playersArray.Length;

            if (heroes.Count % playersArray.Length != 0)
            {
                Debug.LogError("Heroes qty does not equally match number of players.");
                return;
            }
            
            for (var index = 0; index < playersArray.Length; index++)
            {
                var player = playersArray[index];
                if (numberOfHeroesPerPlayer != player.GetViews().Count)
                {
                    Debug.LogError("HeroViews qty does not match number of Heroes in the Config.");
                    return;
                }
                IReadOnlyList<HeroView> heroViews = player.GetViews();
                HeroEntitiesCollection heroEntitiesCollection = new HeroEntitiesCollection();
                PlayerEntity playerEntity = new PlayerEntity(
                    index,
                    playersArray[index],
                    heroViews,
                    heroEntitiesCollection);

                for (int i = 0; i < numberOfHeroesPerPlayer; i++)
                {
                    int randomIndex = Random.Range(0, heroes.Count);
                    CreateHeroEntities(
                        i, 
                        playerEntity, 
                        heroes[randomIndex], 
                        playersArray[index], 
                        heroEntitiesCollection,
                        heroViews);
                    
                    heroes.RemoveAt(randomIndex);
                }
                _playerEntityDict.Add(index, playerEntity);
            }
        }

        private void CreateHeroEntities(
            int index,
            PlayerEntity playerEntity,
            HeroConfig heroConfig, 
            HeroListView player,
            HeroEntitiesCollection entitiesCollection,
            IReadOnlyList<HeroView> heroViews)
        {
            if (index < 0 || index >= heroViews.Count)
            {
                Debug.LogError($"Index {index} is out of bounds for heroViews with count {heroViews.Count}.");
                return;
            }
            
            heroViews[index].SetIcon(heroConfig.heroImage);
            heroViews[index].name = heroConfig.heroImage.name;
            AbilityComponent abilityComponent = new AbilityComponent
            {
                Effect = heroConfig.effect
            };
                
            HeroEntity heroEntity = new HeroEntity(
                playerEntity,
                heroConfig.health,
                heroConfig.attack,
                heroViews[index],
                abilityComponent);
                
            HeroPair heroPair = new HeroPair(heroViews[index], heroEntity);
            heroEntity.UIComponent.SetStats();
            entitiesCollection.AddHeroPair(heroPair);
        }
        
        public Dictionary<int,PlayerEntity> GetPlayerEntitiesDict()
        {
            return _playerEntityDict;
        }
    }
}