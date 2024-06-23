using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveAgent : MoveComponent, IGameFixedUpdateListener
    {
        private Rigidbody2D _rigidbody2d;
        private InputManager _inputManager;

        private void Awake()
        {
            if (TryGetComponent<Rigidbody2D>(out _rigidbody2d) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
            _inputManager = GetComponent<Character>().InputManager;
            if(_inputManager == null )
                Debug.LogError($"{this.name} is missing InputManager");
        }
        public void OnFixedUpdate()
        {
            MoveByRigidbodyVelocity(_rigidbody2d, new Vector2(_inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}
