using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObserver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HitPointsComponentMono hitPointsComponent;

    private void OnEnable()
    {
        hitPointsComponent.hpEmpty += this.OnCharacterDeath;
    }

    private void OnDisable()
    {
        hitPointsComponent.hpEmpty -= this.OnCharacterDeath;
    }

    private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();
}
