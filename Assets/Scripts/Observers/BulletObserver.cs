using ShootEmUp;
using UnityEngine;


public class BulletObserver
{
    private BulletPool bulletPool;
    private CollisionCheckAgent collisionCheckAgent;
    private LevelBoundsCheckComponent levelBoundsCheckComponent;

    private RegisterListenersComponent registerListeners;

    public BulletObserver(BulletPool bulletPool, CollisionCheckAgent collisionCheckAgent, LevelBoundsCheckComponent levelBoundsCheckComponent) 
    {
        this.bulletPool = bulletPool;
        this.collisionCheckAgent = collisionCheckAgent;
        this.levelBoundsCheckComponent = levelBoundsCheckComponent;

        this.collisionCheckAgent.OnCollisionEntered += HandleDisableEvent;
        this.levelBoundsCheckComponent.OnOutOfBounds += HandleDisableEvent;

        this.registerListeners = new RegisterListenersComponent();
}

    public void Subscribe(GameObject obj)
    {
        this.registerListeners.RegisterListeners(obj);
        //obj.TryGetComponent<CollisionCheckComponentMono>(out CollisionCheckComponentMono collisionCheckComponentMono);
        //if (collisionCheckComponentMono != null)
        //{
        //    collisionCheckComponentMono.OnCollisionEntered += this.HandleDisableEvent;
        //}
        //obj.TryGetComponent<CollisionCheckWrapper>(out CollisionCheckWrapper collisionCheckWrapper);
      
        //obj.TryGetComponent<LevelBoundsCheckMono>(out LevelBoundsCheckMono component);
        //if (component != null)
        //    component.OnOutOfBounds += this.HandleDisableEvent;
        //levelBoundsCheckComponent.OnOutOfBounds += this.HandleDisableEvent;
    }

    protected void HandleDisableEvent(GameObject obj)
    {
        //Debug.Log("calling Handle Disable event");
        bulletPool.EnqueueItem(obj);
        //Debug.Log($"{obj.name} disabled");
    }
}

//public class BulletObserver : ObjectsObserver
//{
//    private Pool bulletPool;
//    private LevelBoundsCheckComponent levelBoundsCheckComponent;
//    private RegisterListenersComponent registerListeners = new RegisterListenersComponent();
//    public BulletObserver(Pool bulletPool, LevelBoundsCheckComponent levelBoundsCheckComponent) : base(bulletPool)
//    {
//        this.bulletPool = bulletPool;
//        this.levelBoundsCheckComponent = levelBoundsCheckComponent;
//    }

//    public override void Subscribe(GameObject bulletObject)
//    {
//        registerListeners.RegisterListeners(bulletObject);
//        bulletObject.TryGetComponent<CollisionCheckComponentMono>(out CollisionCheckComponentMono collisionCheckComponentMono);
//        if(collisionCheckComponentMono != null)
//        {
//            collisionCheckComponentMono.OnCollisionEntered += this.HandleDisableEvent;
//        }
//        bulletObject.TryGetComponent<CollisionCheckWrapper>(out CollisionCheckWrapper collisionCheckWrapper);
//        if(collisionCheckWrapper != null)
//        {
//            collisionCheckWrapper.On
//        }
//        bulletObject.TryGetComponent<LevelBoundsCheckMono>(out LevelBoundsCheckMono component);
//        if(component != null)
//            component.OnOutOfBounds += this.HandleDisableEvent;
//        levelBoundsCheckComponent.OnOutOfBounds += this.HandleDisableEvent;
//    }

//    protected override void HandleDisableEvent(GameObject bulletObject)
//    {
//        registerListeners.UnregisterListeners(bulletObject);
//        bulletObject.GetComponent<CollisionCheckComponentMono>().OnCollisionEntered -= this.HandleDisableEvent;

//        bulletObject.TryGetComponent<LevelBoundsCheckMono>(out LevelBoundsCheckMono component);
//        if (component != null)
//            component.OnOutOfBounds -= this.HandleDisableEvent;
//        levelBoundsCheckComponent.OnOutOfBounds -= this.HandleDisableEvent;
//        bulletPool.EnqueueItem(bulletObject);
//    }
//}
