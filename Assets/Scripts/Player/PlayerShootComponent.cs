using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PlayerShootComponent
    {
        private InputManager inputManager;
        private PlayerBulletSpawnerComponent bulletSpawner;
        private WeaponComponent weaponComponent;
        private Transform player;

        public PlayerShootComponent
            (
            InputManager inputManager, 
            PlayerBulletSpawnerComponent playerBulletSpawnerComponent,
            WeaponComponent weaponComponent,
            [Inject(Id =IdCollection.playerId)] Transform player
            )
        {
            this.bulletSpawner = playerBulletSpawnerComponent;
            this.inputManager = inputManager;
            this.weaponComponent = weaponComponent;
            this.player = player;

            this.inputManager.OnSpacePressedEvent += SpacePressedEventHandler;

        }
        void SpacePressedEventHandler()
        {
            this.bulletSpawner.ShootBullet(this.weaponComponent.GetFirePoint(this.player.gameObject), null);
        }
    }

}
