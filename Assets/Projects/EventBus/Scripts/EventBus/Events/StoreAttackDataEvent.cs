namespace EventBus
{
    public struct StoreAttackDataEvent
    {
        public HeroEntity Target;
        public int DamageTaken;

        public StoreAttackDataEvent(HeroEntity target, int damageTaken)
        {
            Target = target;
            DamageTaken = damageTaken;
        }
    }
}