using System;
using Cysharp.Threading.Tasks;

namespace RealTime
{
    public sealed class ChestLocker
    {
        public event Action OnChestLocked;
        private readonly ChestActivator _chestActivator;

        public ChestLocker(ChestActivator chestActivator)
        {
            _chestActivator = chestActivator;
        }

        private async UniTask InitiateChestLockAnimation(Chest chest)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            chest.CloseChest();
        }

        public void LockChest(Chest chest)
        {
            InitiateChestLockAnimation(chest).Forget();
            chest.SetIsUnlocked(false);
            _chestActivator.ActivateChest(chest.TimerMinutes.Minutes, chest);
            OnChestLocked?.Invoke();
        }
    }
}