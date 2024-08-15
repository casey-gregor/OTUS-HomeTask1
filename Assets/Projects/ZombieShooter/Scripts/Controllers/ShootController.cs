using Atomic.Extensions;
using UnityEngine;

namespace ZombieShooter
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _character.GetAction(CharacterAPIKeys.SHOOT_REQUEST).Invoke();
        }
    }
}