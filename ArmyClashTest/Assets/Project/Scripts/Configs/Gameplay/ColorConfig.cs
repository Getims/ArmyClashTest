using System.Collections.Generic;
using Project.Scripts.Core.Infrastructure.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Configs.Gameplay
{
    public class ColorConfig : ScriptableConfig
    {
        [SerializeField, MinValue(0)]
        private Color _color = Color.black;

        [SerializeField]
        private List<StatConfig> _statConfigs = new List<StatConfig>();

        public Color Color => _color;

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