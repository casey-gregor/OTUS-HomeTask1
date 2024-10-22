namespace EventBus
{
    public struct UpdateTargetEvent
    {
        public HeroEntity Target;

        public UpdateTargetEvent(HeroEntity target)
        {
            Target = target;
        }
    }
}