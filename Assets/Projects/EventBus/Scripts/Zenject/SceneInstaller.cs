using UI;
using Zenject;

namespace EventBus
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneClasses(Container);
            BindEventHandlers(Container);
            BindEffectHandlers(Container);

        }

        private void BindSceneClasses(DiContainer container)
        {
            container.Bind<LogicPipeline>().AsSingle().NonLazy();
            container.Bind<VisualPipeline>().AsSingle().NonLazy();
            container.Bind<ExtraPipeline>().AsSingle().NonLazy();
            container.Bind<EventBus>().AsSingle().NonLazy();
            container.Bind<PipelineContext>().AsSingle().NonLazy();
            container.Bind<UIService>().FromComponentInHierarchy().AsSingle();
            container.Bind<PlayerEntitiesInitiator>().AsSingle().NonLazy();
            container.Bind<PipelineRunner>().FromComponentInHierarchy().AsSingle();
            container.BindInterfacesAndSelfTo<LogicPipelineInstaller>().AsSingle();
        }

        private void BindEffectHandlers(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<HollyShieldEffectHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<ExtraRandomHitHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<AvoidHitWhenAttackingHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<HitOtherRandomTargetHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<GetHealthFromEnemyEffectHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<FreezeTargetEffectHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<HealAllyHeroHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<DamageAllWhenHitHandler>().AsSingle();
        }

        private void BindEventHandlers(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<DealDamageHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<AttackEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<GameOverEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<CheckTargetEffectsEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<CheckAttackerEffectsEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<HealEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<CheckIfDeadEventHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<AddVisualTaskHandler>().AsSingle();
            
            container.BindInterfacesAndSelfTo<StoreEffectsDataService>().AsSingle();
            container.BindInterfacesAndSelfTo<StoreAttackDataService>().AsSingle();
        }
    }
    
}
