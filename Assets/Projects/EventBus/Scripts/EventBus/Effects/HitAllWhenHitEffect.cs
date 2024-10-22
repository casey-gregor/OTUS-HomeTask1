using System.Collections.Generic;

namespace EventBus
{
    public sealed class HitAllWhenHitEffect : IEffect
    {
        public string Name = "Hit all enemy heroes, when attacked";
        public int ExtraDamage = 1;
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
            return $"{Source.View.name} executed special {effectType} ability - {EffectName}.";
        }
        
    }
}