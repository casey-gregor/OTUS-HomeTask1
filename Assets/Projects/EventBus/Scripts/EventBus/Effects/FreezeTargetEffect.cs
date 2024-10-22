namespace EventBus
{
    public sealed class FreezeTargetEffect : IEffect
    {
        public string Name = "Freeze target when attacking";
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

        public EffectType EffectType = EffectType.Offensive;
        public string GetMessage()
        {
            string effectType = EffectType.ToString();
            if (EffectType == EffectType.Any)
                effectType = "";

            return $"{Source.View.name} executed special {effectType} ability - {EffectName}.\n"+ 
                   $"{Target.View.name} is frozen, it will skip 1 turn.";
        }
        
    }
}