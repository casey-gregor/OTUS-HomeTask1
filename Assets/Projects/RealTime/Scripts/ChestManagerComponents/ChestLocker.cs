using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

        private async UniTask LockChest(ChestModel chestModel)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            chestModel.GetChestPresenter().CloseChest();
            chestModel.SetIsUnlocked(false);
            _chestActivator.ActivateChest(chestModel.InitialTimer.Minutes, chestModel);
            OnChestLocked?.Invoke();
        }

        public void InitiateChestLock(ChestModel chestModel)
        {
            LockChest(chestModel).Forget();
        }
    }
}