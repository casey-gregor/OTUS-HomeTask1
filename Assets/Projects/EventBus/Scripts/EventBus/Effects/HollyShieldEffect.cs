
namespace EventBus
{
    public sealed class HollyShieldEffect : IEffect
    {
        public string Name = "Holly shield";
        public int AvailableQty = 1;
        public EffectType EffectType = EffectType.Any;
        public string EffectName
        {
            get => Name;
            set => Name = value; 
        }
        public EffectType Type
        {
            get => EffectType;
            set => EffectType = value; 
        }

        public bool RaisedSuccessfully { get; set; }
        public HeroEntity Source { get; set; }
        public HeroEntity Target { get; set; }

        public string GetMessage()
        {
            return $"{Source.View.name} executed special ability - {EffectName}.\n"+ 
                   $"{Source.View.name} got no damage.";
        }

    }
}