
namespace EventBus
{
    public sealed class EffectsUsedData
    {
        public readonly HeroEntity Invoker;
        public readonly HeroEntity Target;
        public readonly IEffect Effect;

        public EffectsUsedData(HeroEntity invoker, IEffect effect, HeroEntity target)
        {
            Invoker = invoker;
            Effect = effect;
            Target = target;
        }
    }
}