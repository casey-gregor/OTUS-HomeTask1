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
                    int randomIndex = UnityEngine.Random.Range(0, heroes.Count);
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
            int id,
            PlayerEntity playerEntity,
            HeroConfig heroConfig, 
            HeroListView player,
            HeroEntitiesCollection entitiesCollection,
            IReadOnlyList<HeroView> heroViews)
        {
            if (id < 0 || id >= heroViews.Count)
            {
                Debug.LogError($"Index {id} is out of bounds for heroViews with count {heroViews.Count}.");
                return;
            }
            
            heroViews[id].SetIcon(heroConfig.heroImage);
            heroViews[id].name = heroConfig.heroImage.name;
            // string stats = $"{heroConfig.attack}/{heroConfig.health}";
            // heroViews[id].SetStats(stats);
            Ability ability = new Ability
            {
                Effect = heroConfig.effect
            };
                
            HeroEntity hero = new HeroEntity(
                id,
                playerEntity,
                heroConfig.health,
                heroConfig.attack,
                heroViews[id],
                ability);
                
            HeroPair heroPair = new HeroPair(heroViews[id], hero);
            hero.SetStats();
            entitiesCollection.AddHeroPair(heroPair);
        }
        
        // public void CreatePlayerEntities(PlayerConfig[] configs)
        // {
        //     HeroListView[] playersArray = _uiService.GetPlayers();
        //     
        //     if (configs.Length != playersArray.Length)
        //     {
        //         throw new ArgumentException("Mismatch between config count and player array length.");
        //     }
        //     
        //     for (int i = 0; i < configs.Length; i++)
        //     {
        //         IReadOnlyList<HeroView> heroViews = playersArray[i].GetViews();
        //         var heroEntitiesCollection = new HeroEntitiesCollection();
        //         
        //         var playerEntity = new PlayerEntity(
        //             i,
        //             playersArray[i], 
        //             heroViews, 
        //             heroEntitiesCollection);
        //         
        //         CreateHeroEntities(playerEntity, configs[i], playersArray[i], heroEntitiesCollection, heroViews);
        //         
        //         _playerEntityDict.Add(i, playerEntity);
        //     }
        // }
        //
        // private void CreateHeroEntities(
        //     PlayerEntity playerEntity,
        //     PlayerConfig config, 
        //     HeroListView player,
        //     HeroEntitiesCollection entitiesCollection,
        //     IReadOnlyList<HeroView> heroViews)
        // {
        //     for(int i = 0; i < config.heroes.Length; i++)
        //     {
        //         heroViews[i].SetIcon(config.heroes[i].heroImage);
        //         heroViews[i].name = config.heroes[i].heroImage.name;
        //         string stats = $"{config.heroes[i].attack}/{config.heroes[i].health}";
        //         heroViews[i].SetStats(stats);
        //         Ability ability = new Ability
        //         {
        //             Effect = config.heroes[i].effect
        //         };
        //         
        //         HeroEntity hero = new HeroEntity(
        //             i,
        //             playerEntity,
        //             config.heroes[i].heroImage,
        //             config.heroes[i].health,
        //             config.heroes[i].attack,
        //             heroViews[i],
        //             ability);
        //         
        //         HeroPair heroPair = new HeroPair(heroViews[i], hero);
        //         entitiesCollection.AddHeroPair(heroPair);
        //     }
        // }
        //
        public Dictionary<int,PlayerEntity> GetPlayerEntitiesDict()
        {
            return _playerEntityDict;
        }
    }
}