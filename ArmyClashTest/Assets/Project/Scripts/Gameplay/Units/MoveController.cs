using System;
using Project.Scripts.Core.Enums;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    [Serializable]
    public class MoveController
    {
        [SerializeField]
        private Transform _unitTransform;

        private UnitInfo _unitInfo;
        private float _speed;

        public Transform Transform => _unitTransform;

        public void Initialize(UnitInfo unitInfo, float speedPointValue)
        {
            _unitInfo = unitInfo;
            _speed = speedPointValue * _unitInfo.GetStat(UnitStat.SPEED);
        }

        public void MoveTowards(float deltaTime)
        {
            var currentPosition = _unitTransform.position;
            var targetPosition = _unitInfo.Target.Position;
            var direction = (targetPosition - currentPosition).normalized;

            _unitTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, _speed * deltaTime);
            _unitTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}