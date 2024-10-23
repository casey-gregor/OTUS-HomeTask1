using System.Collections.Generic;
using UI;

namespace EventBus
{
    public sealed class HeroEntitiesCollection
    {
        private readonly List<HeroPair> _heroPairs = new();
        
        public void AddHeroPair(HeroPair heroPair)
        {
            _heroPairs.Add(heroPair);
        }
        
        public bool TryGetHeroEntity(int index, out HeroEntity result)
        {
            if (index < 0 || index >= _heroPairs.Count || _heroPairs[index].Entity == null)
            {
                result = null;
                return false;
            }

            result = _heroPairs[index].Entity;
            return true;
        }
        
        public bool TryGetHeroView(int index, out HeroView result)
        {
            if (index < 0 || index >= _heroPairs.Count || _heroPairs[index].View == null)
            {
                result = null;
                return false;
            }

            result = _heroPairs[index].View;
            return true;
        }

        public int GetAllHeroesCount()
        {
            return _heroPairs.Count;
        }

        public int GetAliveHeroesCount()
        {
            int result = 0;
            foreach (HeroPair hero in _heroPairs)
            {
                if (!hero.Entity.HealthComponent.IsDead)
                {
                    result++;
                }
        
            }
            return result;
        }
        
        public List<HeroEntity> GetAliveHeroes()
        {
            List<HeroEntity> result = new();
            foreach (HeroPair hero in _heroPairs)
            {
                if (!hero.Entity.HealthComponent.IsDead)
                {
                    result.Add(hero.Entity);
                }
            }
            return result;
        }
    }
}