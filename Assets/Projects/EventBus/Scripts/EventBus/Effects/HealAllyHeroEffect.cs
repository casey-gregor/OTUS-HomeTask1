namespace EventBus
{
    public sealed class HealAllyHeroEffect : IEffect
    {
        public string Name = "Heal a random ally hero when attacking";
        public int HealAmount = 1;
        public string EffectName
        {
            get => Name;
            set => Name = value; 
        }
        public HeroEntity Source { get; set; }
        public HeroEntity Target { get; set; }
        public HeroEntity Ally { get; set; }
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

            return $"{Source.View.name} executed special {effectType} ability - {EffectName}.\n"+ 
                   $"{Ally.View.name} was healed by 1 healthPoint.";
        }
        
    }
}