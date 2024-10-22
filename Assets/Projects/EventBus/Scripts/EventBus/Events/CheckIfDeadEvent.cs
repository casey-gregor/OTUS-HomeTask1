namespace EventBus
{
    public struct CheckIfDeadEvent
    {
        public HeroEntity Target;

        public CheckIfDeadEvent(HeroEntity target)
        {
            Target = target;
        }
    }
}