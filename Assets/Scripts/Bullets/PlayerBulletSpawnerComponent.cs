using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerBulletSpawnerComponent
    {
        private BulletSpawnerConfig bulletSpawnerConfig;
        private BulletPool bulletPool;
        private BulletConfig bulletConfig;
        private Transform parent;

        public BulletConfig BulletConfig => bulletConfig;
        public BulletPool BulletPool => bulletPool;
        public event Action<GameObject, BulletConfig, Transform, Transform> bulletSpawnedEvent;

        public PlayerBulletSpawnerComponent(BulletSpawnerConfig bulletSpawnerConfig, BulletConfig bulletConfig, 
            Transform parent, DiContainer diContainer)
        { 
            this.bulletSpawnerConfig = bulletSpawnerConfig;
            this.bulletConfig = bulletConfig;
            this.parent = parent;
            this.bulletPool = new BulletPool(this.bulletSpawnerConfig, this.parent, diContainer);

        }


        public void ShootBullet(Transform startPosition, Transform target)
        {
            GameObject bulletObject = this.bulletPool.GetItem();
            bulletSpawnedEvent?.Invoke(bulletObject, this.bulletConfig, startPosition, target);
        }

    }

    //public sealed class BulletSpawnerComponent
    //{

    //    private BulletSpawnerConfig bulletSpawnerConfig;
    //    private Transform worldTransform;
    //    private BulletSpawnerContainers parent;
    //    private LevelBoundsComponent levelBounds;
    //    private BulletPool enemyBulletPool;
    //    private BulletPool playerBulletPool;
    //    private BulletObserver enemyBulletObserver;
    //    private BulletObserver playerBulletObserver;
    //    private LevelBoundsCheckComponent levelBoundsCheckComponent;
    //    private EnemyBulletCollisionCheckComponent collisionCheckAgent;
    //    private DiContainer diContainer;
    //    private Transform target;
    //    private WeaponComponent weapon;

    //    public event Action<GameObject> bulletSpawnEvent;
    //    public event Action<GameObject, Vector2, Vector2> bulletInitializeEvent;

    //    public BulletSpawnerComponent(
    //        LevelBoundsComponent levelBounds,
    //        [Inject(Id = "WorldTransform")] Transform worldTransform,
    //        DiContainer diContainer,
    //        BulletSpawnerConfig bulletSpawnerConfig,
    //        LevelBoundsCheckComponent levelBoundsCheckComponent,
    //        EnemyBulletCollisionCheckComponent collisionCheckAgent,
    //        BulletSpawnerContainers parent,
    //        [Inject(Id = BindingIds.playerId)] Transform target,
    //        WeaponComponent weapon)
    //    {
    //        this.levelBounds = levelBounds;
    //        this.worldTransform = worldTransform;
    //        this.diContainer = diContainer;
    //        this.bulletSpawnerConfig = bulletSpawnerConfig;
    //        this.levelBoundsCheckComponent = levelBoundsCheckComponent;
    //        this.collisionCheckAgent = collisionCheckAgent;
    //        this.parent = parent;
    //        //Debug.Log("parent : " + this.parent.name);
    //        this.target = target;

    //        this.enemyBulletPool = new BulletPool(
    //            this.bulletSpawnerConfig.forEnemy.prefab,
    //            this.bulletSpawnerConfig.forEnemy.initialCount,
    //            this.parent.forEnemy,
    //            this.diContainer);
    //        this.enemyBulletObserver = new BulletObserver(this.enemyBulletPool, this.collisionCheckAgent, this.levelBoundsCheckComponent);

    //        this.playerBulletPool = new BulletPool(
    //            this.bulletSpawnerConfig.forPlayer.prefab,
    //            this.bulletSpawnerConfig.forPlayer.initialCount,
    //            this.parent.forPlayer,
    //            this.diContainer);
    //        this.playerBulletObserver = new BulletObserver(this.playerBulletPool, this.collisionCheckAgent, this.levelBoundsCheckComponent);


    //        this.levelBoundsCheckComponent.SubscribeToSpawner(this);
    //        this.weapon = weapon;
    //    }


    //    public void ShootEnemyBullet(GameObject enemyObj)
    //    {
    //        GameObject bulletObject = this.enemyBulletPool.GetItem();
    //        //Debug.Log("bulletSpawner got : " + bulletObject.name);
    //        enemyBulletObserver.Subscribe(bulletObject);
    //        bulletObject.transform.SetParent(this.worldTransform);

    //        Vector2 startPosition = this.weapon.GetFirePoint(enemyObj).position;
    //        Vector2 vector = this.target != null ? (Vector2)this.target.position - startPosition : Vector2.up;
    //        Vector2 endPosition = this.weapon.Rotation * vector.normalized;
    //        bulletInitializeEvent?.Invoke(bulletObject, startPosition, endPosition);
    //        bulletSpawnEvent?.Invoke(bulletObject);
    //        //Debug.Log($"{bulletObject.name} spawned");
    //    }

    //    public void ShootPlayerBullet(GameObject playerObj)
    //    {
    //        GameObject bulletObject = this.playerBulletPool.GetItem();
    //        //Debug.Log("bulletSpawner got : " + bulletObject.name);
    //        playerBulletObserver.Subscribe(bulletObject);
    //        bulletObject.transform.SetParent(this.worldTransform);

    //        Vector2 startPosition = this.weapon.GetFirePoint(playerObj).position;
    //        Vector2 vector = Vector2.up;
    //        Vector2 endPosition = this.weapon.Rotation * vector.normalized;
    //        bulletInitializeEvent?.Invoke(bulletObject, startPosition, endPosition);
    //        bulletSpawnEvent?.Invoke(bulletObject);
    //        //Debug.Log($"{bulletObject.name} spawned");
    //    }

    //    public void SetTarget(Transform target)
    //    {
    //        this.target = target;
    //    }
    //}
}
