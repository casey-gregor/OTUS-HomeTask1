using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(
    fileName ="EnemySpawnerConfigInstaller", 
    menuName = "SOInstallers/New EnemySpawnerSOInstaller"
    )]
public class EnemySpawnerConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private EnemySpawnerConfig spawnerConfig;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpawnerConfig>().FromInstance(spawnerConfig);
    }
}
