using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShootAgent : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private BulletSpawner bulletSystem;
    [SerializeField] private WeaponComponent weaponComponent;
    private void Awake()
    {
       inputManager.OnSpacePressedEvent += SpacePressedEventHandler;
    }

    void SpacePressedEventHandler()
    {
        bulletSystem.ShootBullet(weaponComponent);
    }

    private void OnDestroy()
    {
        inputManager.OnSpacePressedEvent -= SpacePressedEventHandler;
    }
}
