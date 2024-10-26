using Zenject;

namespace UpgradesManager.Zenject
{
    public class SceneContext : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Conveyor>().FromComponentInHierarchy().AsSingle();
        }
    }
}