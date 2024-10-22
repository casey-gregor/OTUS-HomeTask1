namespace EventBus
{
    public struct AddAttackVisualTasksEvent
    {
        public readonly HeroEntity Attacker;
        public readonly HeroEntity Target;

        public AddAttackVisualTasksEvent(HeroEntity attacker, HeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}