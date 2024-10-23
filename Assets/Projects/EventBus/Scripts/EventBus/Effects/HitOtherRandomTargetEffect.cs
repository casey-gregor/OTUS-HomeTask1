namespace EventBus
{
    public sealed class HitOtherRandomTargetEffect : IEffect
    {
        public string Name = "Hit a wrong random target, when attacking";
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
                   $"{Target.View.name} was hit by mistake.";
        }
        
    }
}