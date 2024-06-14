using ShootEmUp;
using System;
using UnityEngine;

public class BulletObserver : ObjectsObserver
{
    private IFactory _bulletFactory;
    public BulletObserver(BulletFactory bulletFactory) : base(bulletFactory)
    {
        this._bulletFactory = bulletFactory;
    }

    public void SubscribeToBullet(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().OnCollisionEntered += this.OnBulletCollision;
        bullet.GetComponent<Bullet>().OnOutOfBounds += this.OnBulletOutOfBounds;
    }

    public override void SubscribeToObject(GameObject obj)
    {
        obj.GetComponent<Bullet>().OnCollisionEntered += this.OnBulletCollision;
        obj.GetComponent<Bullet>().OnOutOfBounds += this.OnBulletOutOfBounds;
    }

    void OnBulletCollision(GameObject bullet)
    {
        _bulletFactory.RemoveObject(bullet);
        bullet.GetComponent<Bullet>().OnCollisionEntered -= this.OnBulletCollision;
    }
    void OnBulletOutOfBounds(GameObject bullet)
    {
        _bulletFactory.RemoveObject(bullet);
        bullet.GetComponent<Bullet>().OnOutOfBounds -= this.OnBulletOutOfBounds;
    }
}
