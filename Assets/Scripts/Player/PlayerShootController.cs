using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerShootController
    {
        private InputManager inputManager;
        private PlayerBulletSpawner bulletSpawner;
        private WeaponComponent weaponComponent;
        private LevelProvider levelProvider;
        private Transform player;

        public PlayerShootController
            (
            InputManager inputManager, 
            PlayerBulletSpawner playerBulletSpawnerComponent,
            WeaponComponent weaponComponent,
            LevelProvider levelProvider
            )
        {
            this.bulletSpawner = playerBulletSpawnerComponent;
            this.inputManager = inputManager;
            this.weaponComponent = weaponComponent;
            this.levelProvider = levelProvider;
            this.player = this.levelProvider.player;

            this.inputManager.OnSpacePressedEvent += SpacePressedEventHandler;

        }
        void SpacePressedEventHandler()
        {
            this.bulletSpawner.ShootBullet(this.weaponComponent.GetFirePoint(this.player.gameObject), null);
        }
    }

}
