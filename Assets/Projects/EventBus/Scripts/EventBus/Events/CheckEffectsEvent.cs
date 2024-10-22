namespace EventBus
{
    public class CheckEffectsEvent
    {
        public readonly HeroEntity CurrentHero;
        public readonly EffectType Type;
        public HeroEntity Target;
        public CheckEffectsEvent(HeroEntity currentHero, HeroEntity target, EffectType type)
        {
            CurrentHero = currentHero;
            Type = type;
            Target = target;
        }
    }
}