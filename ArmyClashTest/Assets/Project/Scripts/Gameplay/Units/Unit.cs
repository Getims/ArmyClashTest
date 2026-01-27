using System;
using Project.Scripts.Configs.Gameplay;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        private UnitVisual _unitVisual;

        private MoveController _moveController;
        private HealthController _healthController;
        private UnitInfo _unitInfo;

        public UnitInfo UnitInfo => _unitInfo;

        [Button]
        public void Initialize(UnitConfig unitConfig, ShapeConfig shapeConfig, SizeConfig sizeConfig,
            ColorConfig colorConfig)
        {
            _unitVisual.SetMesh(shapeConfig.ShapeMesh, shapeConfig.ShapeBaseScale);
            _unitVisual.SetColor(colorConfig.Color);
            _unitVisual.SetModelSize(sizeConfig.ModelSize);

            _unitInfo = new UnitInfo();
            _unitInfo.AddStats(unitConfig.GetAllStats());
            _unitInfo.AddStats(shapeConfig.StatConfigs);
            _unitInfo.AddStats(sizeConfig.StatConfigs);
            _unitInfo.AddStats(colorConfig.StatConfigs);
        }

        public void Hit(int damage)
        {
            //hit to healthcontroller and check dead
        }

        public void UpdateMove(float deltaTime)
        {
            //_moveController logic
        }

        public void SetTarget(Unit target)
        {
            //_moveController target and hit target
        }
    }

    [Serializable]
    public class MoveController
    {
        //logic
    }

    [Serializable]
    public class HealthController
    {
        //logic  
    }
}