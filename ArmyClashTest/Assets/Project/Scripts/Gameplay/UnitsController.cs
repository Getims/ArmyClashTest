using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using Project.Scripts.Core.Enums;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Gameplay.Factory;
using Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay
{
    public class UnitsController : MonoBehaviour
    {
        [Inject] private IConfigsProvider _configsProvider;

        private Dictionary<UnitTeam, List<IUnit>> _unitsDictionary = new Dictionary<UnitTeam, List<IUnit>>();
        private IUnitsFactory _unitsFactory;
        private int _unitsCountPerTeam;
        private State _state = State.Initialization;

        public event Action OnOneTeamAlive;

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
            _state = State.Work;
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

        private void Update()
        {
            if (_state != State.Work)
                return;

            int aliveTeamsCount = 0;

            foreach (var kvp in _unitsDictionary)
            {
                UpdateUnitsTeam(kvp.Key, kvp.Value);
                if (kvp.Value.Count > 0)
                    aliveTeamsCount++;
            }

            if (aliveTeamsCount == 1)
            {
                OnOneTeamAlive?.Invoke();
                _state = State.Stop;
            }
        }

        private void UpdateUnitsTeam(UnitTeam team, List<IUnit> units)
        {
            var unitsToRemove = new List<IUnit>();
            foreach (var unit in units)
            {
                if (unit.IsAlive == false)
                {
                    unitsToRemove.Add(unit);
                    continue;
                }

                if (unit.HasTarget == false)
                {
                    var enemyUnits = GetEnemyUnits(team);
                    unit.SetTarget(FindClosestEnemy(unit, enemyUnits));
                }

                if (unit.HasTarget == false)
                    continue;

                if (unit.CanAttack())
                    unit.Attack();
                else
                    unit.MoveTowards(Time.deltaTime);
            }

            foreach (var unit in unitsToRemove)
                RemoveUnit(unit, team);
        }

        private void RemoveUnit(IUnit unit, UnitTeam team)
        {
            if (_unitsDictionary.TryGetValue(team, out var units))
                units.Remove(unit);

            _unitsFactory.ReturnUnit(unit);
        }

        private List<IUnit> GetEnemyUnits(UnitTeam team)
        {
            var enemies = new List<IUnit>();
            foreach (var kvp in _unitsDictionary)
            {
                if (kvp.Key != team)
                    enemies.AddRange(kvp.Value);
            }

            return enemies;
        }

        private IUnit FindClosestEnemy(IUnit unit, List<IUnit> enemies)
        {
            IUnit closest = null;
            float minDist = float.MaxValue;
            foreach (var enemy in enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                float distance = Vector3.Distance(unit.Position, enemy.Position);
                if (distance < minDist)
                {
                    minDist = distance;
                    closest = enemy;
                }
            }

            return closest;
        }

        private void CreateUnits(UnitTeam unitTeam, int unitsCountPerTeam)
        {
            bool hasTeam = _unitsDictionary.TryGetValue(unitTeam, out var units);
            if (units == null)
                units = new List<IUnit>();

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

        private enum State
        {
            Initialization,
            Work,
            Stop
        }
    }
}