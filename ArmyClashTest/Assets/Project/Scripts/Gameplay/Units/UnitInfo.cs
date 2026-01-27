using System.Collections.Generic;
using Project.Scripts.Configs.Gameplay;

namespace Project.Scripts.Gameplay.Units
{
    public class UnitInfo
    {
        private Dictionary<UnitStat, int> _stats = new Dictionary<UnitStat, int>();

        public void AddStat(UnitStat unitStat, int value)
        {
            if (_stats.TryGetValue(unitStat, out var stat))
                _stats[unitStat] += value;
            else
                _stats.Add(unitStat, value);
        }

        public void AddStats(IReadOnlyCollection<StatConfig> statConfigs)
        {
            foreach (var statConfig in statConfigs)
                AddStat(statConfig.UnitStat, statConfig.Value);
        }

        public int GetStat(UnitStat unitStat)
        {
            if (_stats.TryGetValue(unitStat, out var stat))
                return stat;
            else
            {
                return 0;
            }
        }
    }
}