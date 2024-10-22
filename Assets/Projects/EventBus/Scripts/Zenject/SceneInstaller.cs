
using EventBus;
using UI;
using UnityEngine.Rendering;
using Zenject;

namespace EventBus
{
    public class SceneInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<LogicPipeline>().AsSingle().NonLazy();
            Container.Bind<VisualPipeline>().AsSingle().NonLazy();
            Container.Bind<ExtraPipeline>().AsSingle().NonLazy();
            Container.Bind<EventBus>().AsSingle().NonLazy();
            Container.Bind<PipelineContext>().AsSingle().NonLazy();
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerEntitiesInitiator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LogicPipelineInstaller>().AsSingle();
            Container.Bind<PipelineRunner>().FromComponentInHierarchy().AsSingle();

            BindEventHandlers(Container);
            BindEffectHandlers(Container);

        }

        private void BindEffectHandlers(DiContainer container)
        {
            Container.BindInterfacesAndSelfTo<HollyShieldEffectHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExtraRandomHitHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AvoidHitWhenAttackingHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HitOtherRandomTargetHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GetHealthFromEnemyEffectHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<FreezeTargetEffectHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealAllyHeroHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HitAllWhenHitHandler>().AsSingle();
        }

        private void BindEventHandlers(DiContainer container)
        {
            Container.BindInterfacesAndSelfTo<DealDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckTargetEffectsEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckAttackerEffectsEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddAttackVisualTasksEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckIfDeadEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateTargetEventHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PreAttackCheckHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CheckEffectsHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<StoreEffectsUseService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StoreAttackDataService>().AsSingle();
        }
    }
    
}
