namespace EventBus
{
    public struct StoreEffectsDataEvent
    {
        public StoreEffectsDataEvent(IEffect effect, HeroEntity invoker, HeroEntity target)
        {
            Effect = effect;
            Invoker = invoker;
            Target = target;
        }

        public IEffect Effect { get; set; }
        public HeroEntity Invoker { get; set; }
        public HeroEntity Target { get; set; }
    }
}