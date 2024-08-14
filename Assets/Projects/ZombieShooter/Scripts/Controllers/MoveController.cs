using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;
using ZombieShooter;

namespace ZombieShooter
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private AtomicEntity _character;


        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            Move(Vector3.zero);

            if (Input.GetKey(KeyCode.W))
            {
                Move(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Move(Vector3.back);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(Vector3.right);
            }

        }

        private void Move(Vector3 direction)
        {
            _character.Get<IAtomicVariable<Vector3>>(CharacterAPIKeys.MOVE_DIRECTION).Value = direction;
        }
    }
}