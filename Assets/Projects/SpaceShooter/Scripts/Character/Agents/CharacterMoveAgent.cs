using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveAgent : MoveComponent, IGameFixedUpdateListener
    {
        [SerializeField] private InputManager inputManager;
        private Rigidbody2D rb;

        private void Awake()
        {
            if (TryGetComponent<Rigidbody2D>(out rb) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
        }
        public void OnFixedUpdate()
        {
            Move(rb, new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}
