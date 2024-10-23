using System.Collections.Generic;

namespace EventBus
{
    public sealed class DamageaAllWhenHitEffect : IEffect
    {
        public string Name = "Hit all enemy heroes, when attacked";
        public int ExtraDamage = 1;
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
            return $"{Source.View.name} executed special ability - {EffectName}.";
        }
        
    }
}