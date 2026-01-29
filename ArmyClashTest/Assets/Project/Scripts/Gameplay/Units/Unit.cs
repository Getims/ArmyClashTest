using Project.Scripts.Configs;
using Project.Scripts.Configs.Gameplay;
using Project.Scripts.Gameplay.Units.Controllers;
using Project.Scripts.Gameplay.Units.Visual;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    public interface IUnit
    {
        GameObject GameObject { get; }
        UnitInfo UnitInfo { get; }
        bool IsAlive { get; }
        bool HasTarget { get; }
        Vector3 Position { get; }

        void SetTarget(IUnit target);
        bool CanAttack();
        void Attack();
        void MoveTowards(float deltaTime);
        void Hit(int damage);
        void StopMoving();
    }

    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField]
        private UnitVisual _unitVisual;

        [SerializeField]
        private MoveControllerAI _moveController;

        private HealthController _healthController;
        private AttackController _attackController;
        private UnitInfo _unitInfo;

        public GameObject GameObject => gameObject;
        public UnitInfo UnitInfo => _unitInfo;
        public bool IsAlive => _healthController.IsAlive;
        public bool HasTarget => _unitInfo.Target != null;
        public Vector3 Position => _moveController.Transform.position;

        [Button]
        public void Initialize(UnitConfig unitConfig, ShapeConfig shapeConfig, SizeConfig sizeConfig,
            ColorConfig colorConfig, GlobalConfig globalConfig, string name)
        {
            gameObject.name = name;

            _unitVisual.SetMesh(shapeConfig.ShapeMesh, shapeConfig.ShapeBaseScale);
            _unitVisual.SetColor(colorConfig.Color);
            _unitVisual.SetModelSize(sizeConfig.ModelSize);

            _unitInfo = new UnitInfo();
            _unitInfo.SetName(name);
            _unitInfo.AddStats(unitConfig.GetAllStats());
            _unitInfo.AddStats(shapeConfig.StatConfigs);
            _unitInfo.AddStats(sizeConfig.StatConfigs);
            _unitInfo.AddStats(colorConfig.StatConfigs);
            _unitInfo.SetSize(sizeConfig.ModelSize);

            _healthController = new HealthController(_unitInfo);
            _healthController.OnHealthChanged += OnHealthChanged;
            OnHealthChanged();

            _moveController.Initialize(_unitInfo, globalConfig.SpeedPointValue);
            _attackController = new AttackController(_unitInfo, globalConfig.AttackSpeedPointValue);
        }

        public void Hit(int damage)
        {
            _healthController.Hit(damage);
        }

        public void StopMoving()
        {
            _moveController.StopMoving();
        }

        public void SetTarget(IUnit target)
        {
            _unitInfo.SetTarget(target);
        }

        public bool CanAttack()
        {
            if (_unitInfo.Target == null)
                return false;

            return ReachAttackDistance();
        }

        public void Attack()
        {
            _attackController.Attack();

            if (UnitInfo.Target != null && UnitInfo.Target.IsAlive == false)
                UnitInfo.SetTarget(null);
        }

        public void MoveTowards(float deltaTime)
        {
            if (_unitInfo.Target == null || ReachAttackDistance())
                _moveController.StopMoving();
            else
                _moveController.MoveTowards(deltaTime);
        }

        private bool ReachAttackDistance()
        {
            float distance = Vector3.Distance(Position, _unitInfo.Target.Position);
            float attackRange = (_unitInfo.Size + _unitInfo.Target.UnitInfo.Size) * 0.55f;

            return distance <= attackRange;
        }

        private void OnHealthChanged()
        {
            _unitVisual.UpdateHealth(_healthController.Health);
        }
    }
}