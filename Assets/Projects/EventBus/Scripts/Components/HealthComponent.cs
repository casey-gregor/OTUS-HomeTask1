namespace Projects.EventBus.Scripts.Components
{
    public class HealthComponent
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public int HealthAfterTurn { get; private set; }
        
        public HealthComponent(int health)
        {
            CurrentHealth = health;
            MaxHealth = health;
            HealthAfterTurn = health;
        }

        public void SetCurrentHealth(int health)
        {
            CurrentHealth = health;
        }

        public void SetHealthAfterTurn(int health)
        {
            HealthAfterTurn = health;
        }
    }
}