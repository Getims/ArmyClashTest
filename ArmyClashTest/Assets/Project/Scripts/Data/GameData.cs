using System;
using System.Collections.Generic;

namespace Project.Scripts.Data
{
    [Serializable]
    public class GameData : Core.Infrastructure.Data.GameData
    {
        public List<TeamData> TeamsInfo = new();
        public int BattlesCount = 0;
    }
}