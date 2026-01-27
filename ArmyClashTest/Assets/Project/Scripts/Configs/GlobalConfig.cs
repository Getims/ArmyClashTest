using System;
using Project.Scripts.Core.Infrastructure.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class GlobalConfig : ScriptableConfig
    {
        [SerializeField]
        private bool _enableDebug;

        [Title("Stats Balance")]
        [SerializeField]
        private float _speedPointValue = 1;

        [SerializeField]
        private float _attackSpeedPointValue = 1;

        public bool EnableDebug => _enableDebug;
        public float SpeedPointValue => _speedPointValue;
        public float AttackSpeedPointValue => _attackSpeedPointValue;
    }
}