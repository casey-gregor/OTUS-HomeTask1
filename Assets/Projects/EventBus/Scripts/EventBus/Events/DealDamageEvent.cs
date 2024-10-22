using UI;

namespace EventBus
{
    public struct DealDamageEvent
    {
        public readonly HeroEntity Attacker;
        public readonly HeroEntity Target;
        
        public readonly int AttackerDamage;

        public DealDamageEvent(HeroEntity attacker = null, HeroEntity target = null, int attackerDamage = default(int))
        {
            Attacker = attacker;
            Target = target;
            
            AttackerDamage = attackerDamage;
        }
    }
}