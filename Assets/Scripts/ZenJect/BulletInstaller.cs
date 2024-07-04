using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
            Container.Bind<Rigidbody2D>().FromComponentOnRoot().AsSingle();
            Container.Bind<SpriteRenderer>().FromComponentOnRoot().AsSingle();
        }
    }

}
