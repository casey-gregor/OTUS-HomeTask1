namespace EventBus
{
    public struct PreAttackCheckEvent
    {
        public HeroEntity Attacker;
        public HeroEntity Target { get; set; }

        public PreAttackCheckEvent(HeroEntity attacker, HeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}