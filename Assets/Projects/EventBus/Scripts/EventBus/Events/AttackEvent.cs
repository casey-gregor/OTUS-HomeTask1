namespace EventBus
{
    public struct AttackEvent
    {
        public readonly HeroEntity Attacker;
        public readonly HeroEntity Target;
        public readonly int Damage;

        public AttackEvent(HeroEntity attacker, HeroEntity target, int damage = default(int))
        {
            Attacker = attacker;
            Target = target;
            
            Damage = damage;
        }
    }
}