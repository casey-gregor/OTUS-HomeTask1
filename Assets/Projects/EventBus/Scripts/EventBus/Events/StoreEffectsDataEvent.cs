namespace EventBus
{
    public struct StoreEffectsDataEvent
    {
        public IEffect Effect { get; set; }
        public StoreEffectsDataEvent(IEffect effect)
        {
            Effect = effect;
        }
    }
}