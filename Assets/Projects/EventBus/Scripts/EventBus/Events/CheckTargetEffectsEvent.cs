namespace EventBus
{
    public struct CheckTargetEffectsEvent
    {
        public readonly HeroEntity Attacker;
        public readonly HeroEntity Target;
        public readonly EffectType Type;

        public CheckTargetEffectsEvent(HeroEntity attacker, HeroEntity target, EffectType effectType)
        {
            Attacker = attacker;
            Target = target; 
            Type = effectType;
        }
    }
}