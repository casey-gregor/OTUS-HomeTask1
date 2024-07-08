using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObserverMono : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHitPointsComponent hitPointsComponent;

    private void OnEnable()
    {
        hitPointsComponent.hpEmptyEvent += this.OnCharacterDeath;
    }

    private void OnDisable()
    {
        hitPointsComponent.hpEmptyEvent -= this.OnCharacterDeath;
    }

    private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();
}
