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
            Container.Bind<StartButton>().AsSingle().WithArguments(startButton).NonLazy();
        }
    }

}
