using UI;

namespace EventBus
{
    public struct HeroPair
    {
        public HeroView View;
        public HeroEntity Entity;

        public HeroPair(HeroView view, HeroEntity entity)
        {
            View = view;
            Entity = entity;
        }
    }
}