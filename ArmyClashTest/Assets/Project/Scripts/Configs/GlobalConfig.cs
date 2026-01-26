using System;
using Project.Scripts.Core.Infrastructure.Configs;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [Serializable]
    public class GlobalConfig : ScriptableConfig
    {
        [SerializeField]
        private bool _enableDebug;

        public bool EnableDebug => _enableDebug;
    }
}