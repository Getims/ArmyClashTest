using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Core.Infrastructure.StateMachines.States;
using Project.Scripts.Gameplay;
using Project.Scripts.UI.Game;
using Project.Scripts.UI.Game.Settings;

namespace Project.Scripts.Core.Scenes.Game.States
{
    public class GamePlayState : IEnterState, IExitState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFlowController _gameFlowController;
        private readonly IGameUIController _gameUIController;

        public GamePlayState(GameStateMachine stateMachine, IGameFlowController gameFlowController,
            IGameUIController gameUIController)
        {
            _stateMachine = stateMachine;
            _gameFlowController = gameFlowController;
            _gameUIController = gameUIController;
        }

        public void Enter()
        {
            _gameFlowController.StartBattle();
            _gameFlowController.OnGameOver += OnGameOver;
            SubscribeUI();
        }

        public void Exit()
        {
            _gameFlowController.OnGameOver -= OnGameOver;
            UnsubscribeUI();
        }

        private void OnGameOver(bool isWin)
        {
            _stateMachine.Enter<GameOverState, bool>(isWin);
        }

        private void SubscribeUI()
        {
            var topGamePanel = _gameUIController.ShowPanel<TopGamePanel>();
            topGamePanel.OnSettingsClick += ShowSettings;

            var settingsPopup = _gameUIController.GetPanel<GameSettingsPopup>();
            settingsPopup.OnRestartClick += RestartLevel;
            settingsPopup.OnExitClick += ExitLevel;
        }

        private void UnsubscribeUI()
        {
            var topGamePanel = _gameUIController.GetPanel<TopGamePanel>();
            topGamePanel.OnSettingsClick -= ShowSettings;

            var settingsPopup = _gameUIController.GetPanel<GameSettingsPopup>();
            settingsPopup.OnRestartClick -= RestartLevel;
            settingsPopup.OnExitClick -= ExitLevel;
        }

        private void ShowSettings()
        {
            _gameUIController.ShowPanel<GameSettingsPopup>();
        }

        private void RestartLevel() =>
            _stateMachine.Enter<ResetGameState>();

        private void ExitLevel()
        {
            _stateMachine.Enter<ExitGameState>();
        }
    }
}