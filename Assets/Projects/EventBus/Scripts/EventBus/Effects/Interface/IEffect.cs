namespace EventBus
{
    public interface IEffect
    {
        public string EffectName { get; set; }
        public HeroEntity Source { get; set; }
        public HeroEntity Target { get; set; }
        public EffectType Type { get; set; }
        public bool RaisedSuccessfully { get; set; }
        public string GetMessage();

    }
}