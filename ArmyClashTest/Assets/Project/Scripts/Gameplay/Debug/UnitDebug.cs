using Project.Scripts.Core.Enums;
using Project.Scripts.Gameplay.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Gameplay.Debug
{
    [RequireComponent(typeof(Unit))]
    public class UnitDebug : MonoBehaviour
    {
        [SerializeField]
        private bool _enableDebug;

        [ShowInInspector, ReadOnly, LabelText("HP")]
        private int _health = 0;

        [ShowInInspector, ReadOnly, LabelText("ATK")]
        private int _attack = 0;

        [ShowInInspector, ReadOnly, LabelText("SPEED")]
        private int _speed = 0;

        [ShowInInspector, ReadOnly, LabelText("ATKSPD")]
        private int _attackSpeed = 0;

        private Unit _unit;

        private void Start()
        {
            _unit = transform.GetComponent<Unit>();
        }

        private void FixedUpdate()
        {
            if (_enableDebug && _unit != null)
            {
                _health = _unit.UnitInfo.GetStat(UnitStat.HP);
                _attack = _unit.UnitInfo.GetStat(UnitStat.ATK);
                _speed = _unit.UnitInfo.GetStat(UnitStat.SPEED);
                _attackSpeed = _unit.UnitInfo.GetStat(UnitStat.ATKSPD);
            }
        }
    }
}