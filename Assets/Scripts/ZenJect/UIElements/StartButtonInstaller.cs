using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace ShootEmUp
{
    public class StartButtonInstaller : MonoInstaller
    {
        [SerializeField] private Button startButton;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartButton>().AsSingle().WithArguments(startButton).NonLazy();
        }
    }

}
