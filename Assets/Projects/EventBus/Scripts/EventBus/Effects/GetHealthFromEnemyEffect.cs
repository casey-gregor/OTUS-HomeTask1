﻿namespace EventBus
{
    public sealed class GetHealthFromEnemyEffect : IEffect
    {
        public string Name = "Get health from target enemy when attacking";
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
                   $"{Source.View.name} has taken {Source.AttackDamage} health points from {Target.View.name}.";
        }
        
    }
}