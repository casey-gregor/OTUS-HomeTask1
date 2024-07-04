using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ToFix
//Currently not used. In Spawner the random attack Position is taken from EnemyPosition directly

public class EnemyRandomAttackPositionAgent
{
    private EnemyPositions enemyPositions;
    private EnemySpawner spawner;
    private EnemyHitPointsAgent hitPointsAgent;

    private bool isAlive;

    public Vector2 position { get; private set; }

    public EnemyRandomAttackPositionAgent(
        EnemyPositions enemyPositions, 
        EnemySpawner spawner, 
        EnemyHitPointsAgent hitPointsAgent)
    {
        this.enemyPositions = enemyPositions;
        this.hitPointsAgent = hitPointsAgent;

        //spawner.enemySpawnedEvent += HandleSpawnEvent;
        hitPointsAgent.hpEmpty += HandleHPTest;
    }

    private void SetPosition()
    {
        position = enemyPositions.RandomAttackPosition().transform.position;
    }

    private void HandleSpawnEvent(GameObject enemy)
    {
        enemy.GetComponent<HitPointsComponentMono>().hpEmpty += HandleHPEmptyEvent;
    }

    private void HandleHPEmptyEvent(GameObject enemy)
    {
        Debug.Log("any object is dead");
        isAlive = false;
        enemy.GetComponent<HitPointsComponentMono>().hpEmpty -= HandleHPEmptyEvent;
    }

    private void HandleHPTest()
    {
        Debug.Log("this object is dead");
    }


}
