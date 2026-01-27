using System.Collections.Generic;
using Lean.Pool;
using Project.Scripts.Configs.Gameplay;
using Project.Scripts.Core.Enums;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Core.Utilities;
using Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.UnitsGeneration
{
    public interface IUnitsFactory
    {
        public Unit GetUnit(UnitTeam team);
        public Unit GetUnit(Vector3 position);
        void ReturnUnit(Unit unit);
    }

    public class UnitsFactory : MonoBehaviour, IUnitsFactory
    {
        [SerializeField]
        private LeanGameObjectPool _unitsPool;

        [SerializeField]
        private Transform _unitsContainer;

        [SerializeField]
        private List<SpawnZone> _spawnZones = new List<SpawnZone>();

        [Inject] private IConfigsProvider _configsProvider;

        private UnitsConfigProvider _unitsConfigProvider;

        public void Initialize()
        {
            _unitsConfigProvider = _configsProvider.GetConfig<UnitsConfigProvider>();
        }

        public Unit GetUnit(UnitTeam unitTeam)
        {
            var zone = transform;
            foreach (var spawnZone in _spawnZones)
            {
                if (spawnZone.UnitTeam == unitTeam && spawnZone.ZoneTransform != null)
                {
                    zone = spawnZone.ZoneTransform;
                    break;
                }
            }

            return GetUnit(SpawnZoneHelper.GetRandomPointInZone(zone));
        }

        public Unit GetUnit(Vector3 position)
        {
            GameObject unitInstance = GetUnitFromPool(position);
            var unit = unitInstance.GetComponent<Unit>();
            InitializeUnit(unit);

            return unit;
        }

        public void ReturnUnit(Unit unit)
        {
            _unitsPool.Despawn(unit.gameObject);
        }

        private void InitializeUnit(Unit unit)
        {
            var unitConfig = _unitsConfigProvider.BaseConfig;
            var shapeConfig = _unitsConfigProvider.Shapes.GetRandomElement();
            var sizeConfig = _unitsConfigProvider.Sizes.GetRandomElement();
            var colorConfig = _unitsConfigProvider.Colors.GetRandomElement();

            unit.Initialize(unitConfig, shapeConfig, sizeConfig, colorConfig);
        }

        private GameObject GetUnitFromPool(Vector3 position)
        {
            return _unitsPool.Spawn(position, Quaternion.identity, _unitsContainer);
        }
    }
}