using System.Collections.Generic;
using Project.Scripts.Core.Infrastructure.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Configs.Gameplay
{
    public class SizeConfig : ScriptableConfig
    {
        [SerializeField, MinValue(0)]
        private float _modelSize = 1;

        [SerializeField]
        private List<StatConfig> _statConfigs = new List<StatConfig>();

        public float ModelSize => _modelSize;
        public IReadOnlyCollection<StatConfig> StatConfigs => _statConfigs;
        public int GetStat(UnitStat unitStat)
        {
            var value = 0;
            foreach (var statConfig in _statConfigs)
            {
                if (statConfig.UnitStat == unitStat)
                    value += statConfig.Value;
            }

            return value;
        }
    }
}