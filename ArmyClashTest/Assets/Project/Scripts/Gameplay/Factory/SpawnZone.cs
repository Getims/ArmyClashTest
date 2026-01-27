using System;
using Project.Scripts.Core.Enums;
using UnityEngine;

namespace Project.Scripts.Gameplay.Units
{
    [Serializable]
    public class SpawnZone
    {
        [SerializeField]
        private UnitTeam _unitTeam;

        [SerializeField]
        private Transform _zoneTransform;

        public UnitTeam UnitTeam => _unitTeam;
        public Transform ZoneTransform => _zoneTransform;
			
    }
}