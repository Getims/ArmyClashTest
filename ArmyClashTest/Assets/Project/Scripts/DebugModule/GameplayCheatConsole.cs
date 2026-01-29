using System.Collections.Generic;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Core.Infrastructure.Data;
using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Data;
using Project.Scripts.DebugModule.Cheats;
using Project.Scripts.DebugModule.Core;
using Project.Scripts.Gameplay;
using Zenject;

namespace Project.Scripts.DebugModule
{
    public class GameplayCheatConsole : ACheatConsole
    {
        [Inject] private IDatabase _database;
        [Inject] private IGameDataService _gameDataService;
        [Inject] private IConfigsProvider _configsProvider;
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private IGameFlowController _gameFlowController;

        public override List<ICheat> CreateCheats()
        {
            _cheats = new List<ICheat>();
            AddCheat(new SetFPSCheat());
            AddCheat(new CompleteLevelCheat(_gameFlowController));
            return _cheats;
        }
    }
}