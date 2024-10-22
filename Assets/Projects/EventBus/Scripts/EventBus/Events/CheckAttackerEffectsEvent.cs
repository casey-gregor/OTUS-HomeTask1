namespace EventBus
{
    public struct CheckAttackerEffectsEvent
    {
        public readonly HeroEntity Attacker;
        public readonly HeroEntity Target;
        public readonly EffectType Type;

        public CheckAttackerEffectsEvent(HeroEntity attacker, HeroEntity target, EffectType type)
        {
            Attacker = attacker;
            Target = target;
            Type = type;
        }
    }
}