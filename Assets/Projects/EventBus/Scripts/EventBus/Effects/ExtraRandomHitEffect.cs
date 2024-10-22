
namespace EventBus
{
    public sealed class ExtraRandomHitEffect : IEffect
    {
        public string Name = "Extra hit of a random enemy";
        public int ExtraDamage = 3;
        public string EffectName
        {
            get => Name;
            set => Name = value; 
        }
        public HeroEntity Source { get; set; }
        public HeroEntity Target { get; set; }
        public HeroEntity RandomTarget { get; set; }
        public EffectType Type
        {
            get => EffectType;
            set => EffectType = value; }

        public EffectType EffectType = EffectType.Offensive;
        public string GetMessage()
        {
            string effectType = EffectType.ToString();
            if (EffectType == EffectType.Any)
                effectType = "";

            return $"{Source.View.name} executed special {effectType} ability - {EffectName} - " + 
                   $"{RandomTarget.View.name}.";
        }
        
    }
}