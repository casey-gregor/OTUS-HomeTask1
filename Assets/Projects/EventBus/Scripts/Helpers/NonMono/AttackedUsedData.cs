﻿namespace EventBus
{
    public class AttackedUsedData
    {
        public readonly HeroEntity Target;
        public readonly int DamageTaken;

        public AttackedUsedData(HeroEntity target, int damageTaken)
        {
            Target = target;
            DamageTaken = damageTaken;
        }
    }
}