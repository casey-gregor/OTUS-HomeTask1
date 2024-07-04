using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName ="LevelbackgroundConfigInstaller", menuName ="SOInstallers/New LevelbackgroundConfigInstaller")]
public class LevelBackgroundConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private LevelbackgroundConfig LevelbackgroundConfig;
    public override void InstallBindings()
    {
        Container.Bind<LevelbackgroundConfig>().FromInstance(LevelbackgroundConfig);
    }
}
