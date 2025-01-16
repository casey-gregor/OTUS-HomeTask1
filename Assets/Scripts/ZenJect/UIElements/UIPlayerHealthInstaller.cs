using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class UIPlayerHealthInstaller : MonoInstaller
    {
        [SerializeField] private GameObject heartIconPrefab;
        [SerializeField] private Transform parent;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerHealthIcons>().AsSingle().WithArguments(heartIconPrefab, parent).NonLazy();
        }
        
    }
}