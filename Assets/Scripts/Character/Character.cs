using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private InputManager inputManager;
        public InputManager InputManager { get { return inputManager; } }
        private WeaponComponent _weaponComponent;

        private void Awake()
        {
            if (TryGetComponent<WeaponComponent>(out _weaponComponent) == false)
                Debug.LogError($"{this.name} is missing WeaponComponent");

            inputManager.OnSpacePressedEvent += SpacePressedEventHandler;
        }

        void SpacePressedEventHandler()
        {
            bulletSystem.ShootBullet(_weaponComponent);
        }

        private void OnDisable()
        {
            inputManager.OnSpacePressedEvent -= SpacePressedEventHandler;
        }
    }
}