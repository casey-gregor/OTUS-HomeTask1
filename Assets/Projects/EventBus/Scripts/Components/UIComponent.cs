
namespace EventBus
{
    public sealed class UIComponent
    {
        private readonly HeroEntity _heroEntity;
        public UIComponent(HeroEntity heroEntity)
        {
            _heroEntity = heroEntity;
        }
        public void SetStats(int attackValue=default, int healthValue=default)
        {
            if (attackValue == default)
            {
                attackValue = _heroEntity.AttackDamage;
            }

            if (healthValue == default)
            {
                healthValue = _heroEntity.HealthComponent.CurrentHealth;
            }
            
            string stats = $"{_heroEntity.View.name}\n {attackValue}/{healthValue}";
            _heroEntity.View.SetStats(stats);
        }
    }
}