using System;
using Project.Scripts.Core.Infrastructure.Data;

namespace Project.Scripts.Data
{
    [Serializable]
    public class LevelsData : GameData
    {
        public int CurrentLevel = 0;
        public int LevelsPass = -1;
        public int CollectionsUnlocked = 0;
    }
}