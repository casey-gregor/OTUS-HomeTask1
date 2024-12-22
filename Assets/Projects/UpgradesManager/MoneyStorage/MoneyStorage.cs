using System;
using UnityEngine;

namespace Game.GamePlay.Upgrades
{
    public class MoneyStorage : MonoBehaviour, IMoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;
        public int Money => _money;

        private int _money;
        
        public void EarnMoney(int amount)
        {
            _money += amount;
            OnMoneyEarned?.Invoke(amount);
            OnMoneyChanged?.Invoke(_money);
        }

        public void SpendMoney(int amount)
        {
            _money -= amount;
            OnMoneySpent?.Invoke(amount);
            OnMoneyChanged?.Invoke(_money);
        }

        public bool CanSpendMoney(int amount)
        {
            var result = amount <= _money;
            return result;
        }
    }
}