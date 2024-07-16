using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class MyPlayerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        public override void InstallBindings()
        {
            Container.Bind<PlayerHitPointsComponent>().AsSingle().NonLazy();
            Container.Bind<PlayerHealthObserverComponent>().AsSingle().NonLazy();
            Container.Bind<PlayerShootController>().AsSingle().NonLazy();
            Container.Bind<PlayerMoveController>().AsSingle().WithArguments(playerRigidbody).NonLazy();
        }
    }

}
