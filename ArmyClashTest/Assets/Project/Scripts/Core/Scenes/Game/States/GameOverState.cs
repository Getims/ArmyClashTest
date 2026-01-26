using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Core.Infrastructure.StateMachines.States;
using Project.Scripts.UI.Game;
using Project.Scripts.UI.Game.Settings;
using UnityEngine;

namespace Project.Scripts.Core.Scenes.Game.States
{
    public class GameOverState : IEnterState<bool>, IExitState
    {
        private readonly GameStateMachine _stateMachine;

        private LevelCompletePanel _levelCompletePanel;
        private IGameUIController _gameUIController;

        public GameOverState(GameStateMachine stateMachine, IGameUIController gameUIController)
        {
            _gameUIController = gameUIController;
            _stateMachine = stateMachine;
        }

        public void Enter(bool isWin)
        {
            HideGameHud();
            if (isWin)
                CreateLevelCompletePanel();
            else
                CreateLevelFailPanel();
        }

        public void Exit()
        {
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

        private void CreateLevelFailPanel()
        {
            Debug.Log("Add level fail panel if needed");
            CreateLevelCompletePanel();
        }

        private void OnLevelComplete()
        {
            _stateMachine.Enter<ExitGameState>();
        }
    }
}