namespace EventBus
{
    public sealed class FreezeTargetEffect : IEffect
    {
        public string Name = "Freeze target when attacking";
        public int SkipTurnNum = 1;
        public EffectType EffectType = EffectType.Offensive;
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
                   $"{Target.View.name} is frozen and will skip {SkipTurnNum} turn(-s).";
        }
        
    }
}