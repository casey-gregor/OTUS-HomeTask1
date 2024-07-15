using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerShootComponent
    {
        private InputManager inputManager;
        private PlayerBulletSpawner bulletSpawner;
        private WeaponComponent weaponComponent;
        private LevelProvider levelProvider;
        private Transform player;

        public PlayerShootComponent
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
