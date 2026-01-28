using System.Collections.Generic;
using Lean.Pool;
using Project.Scripts.Configs;
using Project.Scripts.Configs.Gameplay;
using Project.Scripts.Core.Enums;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Core.Utilities;
using Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay.Factory
{
    public interface IUnitsFactory
    {
        public IUnit GetUnit(UnitTeam team);
        public IUnit GetUnit(Vector3 position);
        void ReturnUnit(IUnit unit);
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
        private GlobalConfig _globalConfig;

        public void Initialize()
        {
            _unitsConfigProvider = _configsProvider.GetConfig<UnitsConfigProvider>();
            _globalConfig = _configsProvider.GetConfig<GlobalConfig>();

            foreach (SpawnZone spawnZone in _spawnZones)
                spawnZone.Initialize(_globalConfig.UnitsCountPerTeam);
        }

        public IUnit GetUnit(UnitTeam unitTeam)
        {
            var zone = GetOrCreateSpawnZone(unitTeam);

            return GetUnit(zone.GetRandomPointInZone());
        }

        public IUnit GetUnit(Vector3 position)
        {
            GameObject unitInstance = GetUnitFromPool(position);
            var unit = unitInstance.GetComponent<Unit>();
            InitializeUnit(unit);

            return unit;
        }

        public void ReturnUnit(IUnit unit)
        {
            _unitsPool.Despawn(unit.GameObject);
        }

        private SpawnZone GetOrCreateSpawnZone(UnitTeam unitTeam)
        {
            SpawnZone zone = null;
            foreach (var spawnZone in _spawnZones)
            {
                if (spawnZone.UnitTeam == unitTeam && spawnZone.ZoneTransform != null)
                {
                    zone = spawnZone;
                    break;
                }
            }

            if (zone == null)
            {
                Debug.LogWarning($"No zones for {unitTeam}! Creating a zone!");
                zone = new SpawnZone(unitTeam, transform);
                zone.Initialize(_globalConfig.UnitsCountPerTeam);
                _spawnZones.Add(zone);
            }

            return zone;
        }

        private void InitializeUnit(Unit unit)
        {
            var unitConfig = _unitsConfigProvider.BaseConfig;
            var shapeConfig = _unitsConfigProvider.Shapes.GetRandomElement();
            var sizeConfig = _unitsConfigProvider.Sizes.GetRandomElement();
            var colorConfig = _unitsConfigProvider.Colors.GetRandomElement();

            string name = $"Unit {shapeConfig.name} {sizeConfig.name} {colorConfig.name}";
            unit.Initialize(unitConfig, shapeConfig, sizeConfig, colorConfig, _globalConfig, name);
        }

        private GameObject GetUnitFromPool(Vector3 position)
        {
            return _unitsPool.Spawn(position, Quaternion.identity, _unitsContainer);
        }
    }
}