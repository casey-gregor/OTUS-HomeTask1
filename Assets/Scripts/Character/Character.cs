using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private InputManager inputManager;
        public BulletSystem BulletSystem { get { return bulletSystem; } }
        public WeaponComponent WeaponComponent { get { return _weaponComponent; } }
        public InputManager InputManager { get { return inputManager; } }

        private WeaponComponent _weaponComponent;
        private void Awake()
        {
            if (inputManager == null)
                Debug.LogError($"{this.name} is missing InputManager");
            if (bulletSystem == null)
                Debug.LogError($"{this.name} is missing BulletSystem");
            if (TryGetComponent<WeaponComponent>(out _weaponComponent) == false)
                Debug.LogError($"{this.name} is missing WeaponComponent");

        }   
    }
}