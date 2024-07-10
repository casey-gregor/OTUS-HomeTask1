using TMPro;
using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public class GameOverTextInstaller : MonoInstaller
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameOverText>().AsSingle().WithArguments(textMeshPro).NonLazy();
        }
    }

}
