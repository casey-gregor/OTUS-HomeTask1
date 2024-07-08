using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckDestinationComponent : IGameFixedUpdateListener
{
    public bool IsReached { get { return this.isReached; } }
    private bool isReached;

    public Vector2 direction { get; private set; }

    private Transform enemyTransform;
    private EnemySpawnerComponent enemySpawner;

    private Dictionary<GameObject, Transform> objectsToCheck;
    private HashSet<GameObject> keysToRemove;

    public event Action<GameObject> destinationReachedEvent;
    public EnemyCheckDestinationComponent(
        EnemySpawnerComponent enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        enemySpawner.enemySpawnedEvent += HandleSpawnEvent;

        objectsToCheck = new Dictionary<GameObject, Transform>();
        keysToRemove = new HashSet<GameObject>();

        IGameListener.Register(this);
    }

    private bool CheckIfReached(GameObject obj)
    {
        direction = (Vector2)objectsToCheck[obj].position - (Vector2)obj.transform.position;
        return this.isReached = direction.magnitude <= 0.25f ? true : false;
    }

    private void HandleSpawnEvent(GameObject obj, Transform attackPosition)
    {
        objectsToCheck.Add(obj, attackPosition);
    }

    public void OnFixedUpdate()
    {
        keysToRemove.Clear();

        foreach(GameObject key in objectsToCheck.Keys)
        {
            if (CheckIfReached(key))
            {
                destinationReachedEvent?.Invoke(key);
                keysToRemove.Add(key);
            }
        }

        foreach(GameObject key in keysToRemove)
        {
            objectsToCheck.Remove(key);
        }
    }
}

//public class EnemyCheckDestinationAgent : IGameFixedUpdateListener
//{
//    public bool IsReached { get { return this.isReached; } }
//    private bool isReached;

//    public Vector2 direction {  get; private set; }

//    private Transform enemyTransform;
//    private EnemyRandomAttackPositionAgent randomAttackPositionAgent;
//    public EnemyCheckDestinationAgent(
//        Transform enemyTransform, 
//        EnemyRandomAttackPositionAgent randomAttackPositionAgent)
//    {
//        this.enemyTransform = enemyTransform;
//        this.randomAttackPositionAgent = randomAttackPositionAgent;

//        IGameListener.Register(this);
//    }

//    private bool CheckIfReached(Transform enemyTransform)
//    {
//        direction = this.randomAttackPositionAgent.position - (Vector2)enemyTransform.position;
//        return this.isReached = direction.magnitude <= 0.25f ? true : false;
//    }

//    public void OnFixedUpdate()
//    {
//        if (this.randomAttackPositionAgent.position == Vector2.zero)
//            return;
//        CheckIfReached(enemyTransform);
//    }
//}
