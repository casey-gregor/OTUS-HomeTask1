
namespace EventBus
{
    public sealed class ExtraRandomHitEffect : IEffect
    {
        public string Name = "Extra hit of a random enemy";
        public int ExtraDamage = 3;
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
        public HeroEntity RandomTarget { get; set; }

        public string GetMessage()
        {
            return $"{Source.View.name} executed special ability - {EffectName} : " + 
                   $"{RandomTarget.View.name}.";
        }
        
    }
}