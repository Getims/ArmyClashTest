using System.Collections.Generic;
using Project.Scripts.Core.Infrastructure.Data;
using Project.Scripts.Data;
using Project.Scripts.DebugModule.Cheats;
using Project.Scripts.DebugModule.Core;
using Zenject;

namespace Project.Scripts.DebugModule
{
    public class MenuCheatConsole : ACheatConsole
    {
        [Inject] private IDatabase _database;
        [Inject] private ILevelsDataService _levelsDataService;

        public override List<ICheat> CreateCheats()
        {
            _cheats = new List<ICheat>();
            AddCheat(new SetFPSCheat());
            AddCheat(new ResetProgressionCheat(_database));
            return _cheats;
        }
    }
}