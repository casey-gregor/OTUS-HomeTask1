using TMPro;
using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public class TextCountdownInstaller : MonoInstaller
    {
        [SerializeField] private TextCountdownConfig config;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TextCountdownComponent>().AsSingle().WithArguments(config, textMeshPro).NonLazy();
        }
    }

}
