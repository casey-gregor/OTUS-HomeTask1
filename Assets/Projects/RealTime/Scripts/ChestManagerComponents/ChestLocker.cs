using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTime
{
    public sealed class ChestLocker
    {
        public event Action<ChestModel> OnChestLocked;

        private const int relockDelay = 5;
        private readonly ChestActivator _chestActivator;

        public ChestLocker(ChestActivator chestActivator)
        {
            _chestActivator = chestActivator;
        }

        private async UniTask LockChest(ChestModel chestModel)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(relockDelay));
            chestModel.GetChestPresenter().CloseChest();
            chestModel.SetIsUnlocked(false);
            Debug.Log("initial timer in lock : " + chestModel.InitialTimer.Minutes);
            _chestActivator.ActivateChest(chestModel.InitialTimer.Minutes, chestModel);
            OnChestLocked?.Invoke(chestModel);
        }

        public void InitiateChestLock(ChestModel chestModel)
        {
            LockChest(chestModel).Forget();
        }
    }
}