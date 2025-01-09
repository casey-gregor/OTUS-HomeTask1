using System;

namespace RealTime
{
    public sealed class ChestLocker
    {
        public event Action<ChestPresenter> OnChestLocked;
        
        private readonly ChestActivator _chestActivator;

        public ChestLocker(ChestActivator chestActivator)
        {
            _chestActivator = chestActivator;
        }

        public void InitiateChestLock(ChestPresenter chestPresenter)
        {
            chestPresenter.CloseChest();
            chestPresenter.SetIsUnlocked(false);
            OnChestLocked?.Invoke(chestPresenter);
        }
    }
}