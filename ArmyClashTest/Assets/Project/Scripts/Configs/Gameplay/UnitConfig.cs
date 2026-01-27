using Project.Scripts.Core.Infrastructure.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Configs.Gameplay
{
    public class UnitConfig : ScriptableConfig
    {
        [SerializeField, LabelText("HP"), MinValue(0)]
        private int _health = 100;

        [SerializeField, LabelText("ATK"), MinValue(0)]
        private int _attack = 10;

        [SerializeField, LabelText("SPEED"), MinValue(0)]
        private int _speed = 10;

        [SerializeField, LabelText("ATKSPD"), MinValue(0)]
        private int _attackSpeed = 1;

        public int GetStat(UnitStat unitStat)
        {
            switch (unitStat)
            {
                case UnitStat.HP:
                    return _health;
                case UnitStat.ATK:
                    return _attack;
                case UnitStat.SPEED:
                    return _speed;
                case UnitStat.ATKSPD:
                    return _attackSpeed;
                default:
                    return 0;
            }
        }
    }
}