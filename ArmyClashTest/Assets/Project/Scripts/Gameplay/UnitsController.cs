using System.Collections.Generic;
using Project.Scripts.Configs;
using Project.Scripts.Core.Enums;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Gameplay.Units;
using Project.Scripts.Gameplay.UnitsGeneration;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay
{
    public class UnitsController : MonoBehaviour
    {
        [Inject] private IConfigsProvider _configsProvider;

        private Dictionary<UnitTeam, List<Unit>> _unitsDictionary = new Dictionary<UnitTeam, List<Unit>>();
        private IUnitsFactory _unitsFactory;
        private int _unitsCountPerTeam;

        public void Initialize(IUnitsFactory unitsFactory)
        {
            _unitsFactory = unitsFactory;

            var globalConfig = _configsProvider.GetConfig<GlobalConfig>();
            _unitsCountPerTeam = globalConfig.UnitsCountPerTeam;
        }

        public void CreateUnits()
        {
            CreateUnits(UnitTeam.Team1, _unitsCountPerTeam);
            CreateUnits(UnitTeam.Team2, _unitsCountPerTeam);
        }

        public void ClearUnits()
        {
            foreach (var kvp in _unitsDictionary)
            {
                foreach (var unit in kvp.Value)
                {
                    _unitsFactory.ReturnUnit(unit);
                }
            }

            _unitsDictionary.Clear();
        }

        private void CreateUnits(UnitTeam unitTeam, int unitsCountPerTeam)
        {
            bool hasTeam = _unitsDictionary.TryGetValue(unitTeam, out var units);
            if (units == null)
                units = new List<Unit>();

            for (int i = 0; i < unitsCountPerTeam; i++)
            {
                var unit = _unitsFactory.GetUnit(unitTeam);
                units.Add(unit);
            }

            if (hasTeam)
                _unitsDictionary[unitTeam] = units;
            else
                _unitsDictionary.Add(unitTeam, units);
        }
    }
}