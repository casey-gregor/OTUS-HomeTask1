using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace ShootEmUp
{
    public class PlayPauseButtonInstaller : MonoInstaller
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayPauseButton>().AsSingle().WithArguments(button, textMeshPro).NonLazy();
        }
    }

}
