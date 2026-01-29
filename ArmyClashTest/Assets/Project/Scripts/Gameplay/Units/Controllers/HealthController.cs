using System;
using Project.Scripts.Core.Enums;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units.Controllers
{
    [Serializable]
    public class HealthController
    {
        private int _health;

        public bool IsAlive => _health > 0;
        public int Health => _health;
        public event Action OnHealthChanged;

        public HealthController(UnitInfo unitInfo)
        {
            _health = unitInfo.GetStat(UnitStat.HP);
            if (_health == 0)
                Debug.LogWarning($"Dead on start {unitInfo.Name}");
        }

        public void Hit(int damage)
        {
            if (_health == 0)
                return;

            _health -= damage;
            if (_health <= 0)
                _health = 0;
            OnHealthChanged?.Invoke();
        }
    }
}