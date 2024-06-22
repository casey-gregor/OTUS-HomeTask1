using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShootAgent : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        character.InputManager.OnSpacePressedEvent += SpacePressedEventHandler;
    }

    void SpacePressedEventHandler()
    {
        character.BulletSystem.ShootBullet(character.WeaponComponent);
    }

    private void OnDestroy()
    {
        character.InputManager.OnSpacePressedEvent -= SpacePressedEventHandler;
    }
}
