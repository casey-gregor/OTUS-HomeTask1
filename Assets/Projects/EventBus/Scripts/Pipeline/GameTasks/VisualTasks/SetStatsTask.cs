using UnityEngine;

namespace EventBus
{
    public sealed class SetStatsTask : GameTask
    {
        private readonly HeroEntity _attacker;
        private readonly HeroEntity _target;

        public SetStatsTask(HeroEntity attacker = default, HeroEntity target = default)
        {
            _attacker = attacker;
            _target = target;
        }

        protected override void OnRun()
        {
            Debug.Log("start set stats");
            if(_attacker != null)
                _attacker.SetStats();
            
            if(_target != null)
                _target.SetStats();

            Finish();
        }
    }
}