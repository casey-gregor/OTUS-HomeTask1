using ShootEmUp;
using System;
using UnityEngine;
using Zenject;

public class CharacterShootAgent : IInitializable, ILateDisposable
{
    private InputManager inputManager;
    private PlayerBulletSpawnerComponent bulletSpawner;
    private WeaponComponent weaponComponent;
    private Transform player;

    public CharacterShootAgent(
        InputManager inputManager, 
        PlayerBulletSpawnerComponent playerBulletSpawnerComponent,
        WeaponComponent weaponComponent,
        [Inject(Id =BindingIds.playerId)] Transform player)
    {
        this.bulletSpawner = playerBulletSpawnerComponent;
        this.inputManager = inputManager;
        this.weaponComponent = weaponComponent;
        this.player = player;
    }
    void SpacePressedEventHandler()
    {
        bulletSpawner.ShootBullet(weaponComponent.GetFirePoint(this.player.gameObject), null);
    }

    public void Initialize()
    {
        inputManager.OnSpacePressedEvent += SpacePressedEventHandler;
    }

    public void LateDispose()
    {
        inputManager.OnSpacePressedEvent -= SpacePressedEventHandler;
    }
}
