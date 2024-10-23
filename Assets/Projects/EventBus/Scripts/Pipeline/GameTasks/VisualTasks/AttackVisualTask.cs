using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class AttackVisualTask : GameTask
    {
        private readonly HeroEntity _attacker;
        private readonly HeroEntity _target;

        public AttackVisualTask(HeroEntity attacker, HeroEntity target)
        {
            _attacker = attacker;
            _target = target;
        }

        protected override void OnRun()
        {
            PerformAttackAnimation(_attacker.View, _target.View).Forget();
        }

        private async UniTask PerformAttackAnimation(HeroView attacker, HeroView target)
        {
            await attacker.AnimateAttack(target);
            
            Finish();
        }
    }
}