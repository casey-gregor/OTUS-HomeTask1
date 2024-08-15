using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class MeleeAttackComponent
    {
        [HideInInspector] public AtomicEvent AttackRequestEvent;
        [HideInInspector] public AtomicEvent AttackActionEvent;

        public AtomicVariable<AtomicObject> TargetObject;
        public AtomicVariable<float> DamageInterval;
        public AtomicVariable<int> DamageAmount;
        public AtomicVariable<int> AttackDistance;

        public void Construct()
        {
            AttackActionEvent.Subscribe(Attack);
        }

        public void Attack()
        {
            TargetObject.Value.GetAction<int>(CharacterAPIKeys.DEDUCT_HITPOINTS).Invoke(DamageAmount.Value);
        }

    }
}