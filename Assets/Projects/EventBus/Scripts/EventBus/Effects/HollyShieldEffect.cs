
namespace EventBus
{
    public sealed class HollyShieldEffect : IEffect
    {
        public string Name = "Holly shield";
        public int AvailableQty = 1;
        public string EffectName
        {
            get => Name;
            set => Name = value; 
        }
        public HeroEntity Source { get; set; }
        public HeroEntity Target { get; set; }
        public EffectType Type
        {
            get => EffectType;
            set => EffectType = value; }

        public EffectType EffectType = EffectType.Any;
        public string GetMessage()
        {
            string effectType = EffectType.ToString();
            if (EffectType == EffectType.Any)
                effectType = "";

            return $"{Source.View.name} executed special {effectType} ability - {EffectName}.\n"+ 
                   $"{Source.View.name} got no damage.";
        }

    }
}