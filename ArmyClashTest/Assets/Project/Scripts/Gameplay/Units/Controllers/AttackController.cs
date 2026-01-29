using System;
using Project.Scripts.Core.Enums;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    [Serializable]
    public class AttackController
    {
        private float _attackDelay = 0;
        private float _lastAttackTime = 0;
        private UnitInfo _unitInfo;

        public event Action OnAttack;

        public AttackController(UnitInfo unitInfo, float attackSpeedPointValue)
        {
            _unitInfo = unitInfo;
            _attackDelay = unitInfo.GetStat(UnitStat.ATKSPD) * attackSpeedPointValue;
        }

        public void Attack()
        {
            float currentTime = Time.time;
            if (currentTime < _lastAttackTime + _attackDelay)
                return;

            _lastAttackTime = currentTime;
            _unitInfo.Target.Hit(_unitInfo.GetStat(UnitStat.ATK));
            OnAttack?.Invoke();
        }
    }
}