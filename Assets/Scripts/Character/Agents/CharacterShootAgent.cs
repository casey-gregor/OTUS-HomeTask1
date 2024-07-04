using ShootEmUp;
using System;
using UnityEngine;
using Zenject;

public class CharacterShootAgent : IInitializable, ILateDisposable
{
    private InputManager inputManager;
    private BulletSpawner bulletSpawner;
    private WeaponComponentMono weaponComponent;

    public CharacterShootAgent(
        [Inject(Id=BindingIds.playerId)] BulletSpawner bulletSpawner, 
        InputManager inputManager, 
        WeaponComponentMono weaponComponent)
    {
        this.bulletSpawner = bulletSpawner;
        this.inputManager = inputManager;
        this.weaponComponent = weaponComponent;
    }
    void SpacePressedEventHandler()
    {
        bulletSpawner.ShootBullet(weaponComponent);
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
