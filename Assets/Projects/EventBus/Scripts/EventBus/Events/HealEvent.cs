namespace EventBus
{
    public struct HealEvent
    {
        public HeroEntity Attacker;
        public HeroEntity Target;
        public int HealAmount;

        public HealEvent(HeroEntity attacker, HeroEntity target, int healAmount)
        {
            Attacker = attacker;
            Target = target;
            HealAmount = healAmount;
        }
    }
}