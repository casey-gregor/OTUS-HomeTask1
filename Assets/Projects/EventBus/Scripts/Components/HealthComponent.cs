namespace Projects.EventBus.Scripts.Components
{
    public class HealthComponent
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsInvincible { get; private set; }
        public bool IsDead { get; private set; }
        public HealthComponent(int health)
        {
            CurrentHealth = health;
            MaxHealth = health;
            IsDead = false;
        }

        public void SetCurrentHealth(int health)
        {
            CurrentHealth = health;
        }
        
        public void DeductHealth(int value)
        {
            if(value < 0) return;
            int currentHealth = CurrentHealth;
            currentHealth -= value;
            if(currentHealth < 0) currentHealth = 0;
            SetCurrentHealth(currentHealth);
        }

        public void AddHealth(int value)
        {
            if(value < 0) return;
            int currentHealth = CurrentHealth;
            currentHealth += value;
            if(currentHealth > MaxHealth) currentHealth = MaxHealth;
            SetCurrentHealth(currentHealth);
        }
        
        public void SetIsDead()
        {
            IsDead = true;
        }
        
        public void SetInvincible(bool value)
        {
            IsInvincible = value;
        }
    }
}