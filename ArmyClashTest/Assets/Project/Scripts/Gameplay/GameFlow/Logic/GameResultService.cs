using Project.Scripts.Data;
using Project.Scripts.Runtime.Enums;

namespace Project.Scripts.Gameplay.GameFlow.Logic
{
    public interface IGameResultService
    {
        void SaveResult();
    }

    public class GameResultService : IGameResultService
    {
        private readonly IGameDataService _gameDataService;
        private readonly IGameInfoService _gameInfoService;

        public GameResultService(IGameDataService gameDataService, IGameInfoService gameInfoService)
        {
            _gameInfoService = gameInfoService;
            _gameDataService = gameDataService;
        }

        public void SaveResult()
        {
            _gameDataService.BattlesCount.Set(_gameDataService.BattlesCount.Value + 1);

            UnitTeam winner = UnitTeam.Team1;
            foreach (var teamInfo in _gameInfoService.UnitsDictionary)
            {
                if (teamInfo.Value > 0)
                {
                    winner = teamInfo.Key;
                    break;
                }
            }

            _gameDataService.AddTeamWin(winner);
        }
    }
}