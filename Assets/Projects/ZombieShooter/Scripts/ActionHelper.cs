using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class ActionHelper : MonoBehaviour
    {
        [SerializeField] private AtomicObject _character;

        public int damage;

        public void DamageCharacter(int damage)
        {
            _character.GetAction<int>(APIKeys.TakeDamageAction).Invoke(damage);
        }
       
    }
}