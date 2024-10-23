using Projects.EventBus.Scripts.Components;
using UI;

namespace EventBus
{
    public class HeroEntity
    {
        public readonly int AttackDamage;
        public readonly HeroView View;
        public readonly PlayerEntity PlayerEntity;
        public readonly HealthComponent HealthComponent;
        public readonly UIComponent UIComponent;
        public readonly AbilityComponent AbilityComponent;
        public readonly TurnManagerComponent TurnComponent;

        public HeroEntity(
            PlayerEntity playerEntity, 
            int health, 
            int attackDamage, 
            HeroView view, 
            AbilityComponent abilityComponent)
        {
            PlayerEntity = playerEntity;
            AttackDamage = attackDamage;
            View = view;
            AbilityComponent = abilityComponent;
            HealthComponent = new HealthComponent(health);
            UIComponent = new UIComponent(this);
            TurnComponent = new();
        }
    }
}