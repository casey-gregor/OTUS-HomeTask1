using System;
using System.Collections.Generic;

namespace EventBus
{
    public class AbilityComponent
    {
        public IEffect Effect;
        
        public bool TryGetEffect(out IEffect effect)
        {
            if (Effect != null)
            {
                effect = Effect;
                return true;
            }

            effect = null;
            return false;
        }
    }
}