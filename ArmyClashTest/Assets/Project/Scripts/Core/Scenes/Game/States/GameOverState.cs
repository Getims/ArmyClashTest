using Project.Scripts.Core.Enums;
using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Core.Infrastructure.StateMachines.States;
using Project.Scripts.Data;
using Project.Scripts.Gameplay;
using Project.Scripts.UI.Game;
using Project.Scripts.UI.Game.Settings;
using Project.Scripts.UI.Game.Top;

namespace Project.Scripts.Core.Scenes.Game.States
{
    public class GameOverState : IEnterState, IExitState
    {
        private readonly GameStateMachine _stateMachine;

        private LevelCompletePanel _levelCompletePanel;
        private IGameUIController _gameUIController;
        private IGameDataService _gameDataService;
        private IGameInfoService _gameInfoService;

        public GameOverState(GameStateMachine stateMachine, IGameUIController gameUIController,
            IGameDataService gameDataService, IGameInfoService gameInfoService)
        {
            _gameInfoService = gameInfoService;
            _gameDataService = gameDataService;
            _gameUIController = gameUIController;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            SaveData();
            HideGameHud();
            CreateLevelCompletePanel();
        }

        public void Exit()
        {
        }

        private void SaveData()
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

        private void HideGameHud()
        {
            _gameUIController.GetPanel<TopGamePanel>().Hide();
            _gameUIController.GetPanel<GameSettingsPopup>().Hide();
        }

        private void CreateLevelCompletePanel()
        {
            if (_levelCompletePanel != null)
                return;

            _levelCompletePanel = _gameUIController.GetPanel<LevelCompletePanel>();
            _levelCompletePanel.OnClaimClick += OnLevelComplete;
            _levelCompletePanel.Show();
        }

        private void OnLevelComplete()
        {
            _stateMachine.Enter<ExitGameState>();
        }
    }
}