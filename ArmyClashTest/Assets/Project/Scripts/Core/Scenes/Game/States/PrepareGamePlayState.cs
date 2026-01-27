using System.Collections;
using Project.Scripts.Core.Constants;
using Project.Scripts.Core.Infrastructure.Services;
using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Core.Infrastructure.StateMachines.States;
using Project.Scripts.Data;
using Project.Scripts.Gameplay;
using Project.Scripts.UI.Game;
using Project.Scripts.UI.Game.Settings;
using UnityEngine;

namespace Project.Scripts.Core.Scenes.Game.States
{
    public class PrepareGamePlayState : IEnterState, IExitState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFlowController _gameFlowController;
        private readonly IGameUIController _gameUIController;

        private Coroutine _levelCreationCO;

        public PrepareGamePlayState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner,
            IGameFlowController gameFlowController, IGameUIController gameUIController)
        {
            _gameUIController = gameUIController;
            _gameFlowController = gameFlowController;
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            if (_levelCreationCO != null)
                _coroutineRunner.StopCoroutine(_levelCreationCO);

            _levelCreationCO = _coroutineRunner.StartCoroutine(CreateLevel());
        }

        public void Exit()
        {
            if (_levelCreationCO != null)
                _coroutineRunner?.StopCoroutine(_levelCreationCO);
        }

        private IEnumerator CreateLevel()
        {
            yield return new WaitForSeconds(GameConstants.SCENE_LOAD_TIME);
            _gameFlowController.Initialize();

            yield return new WaitForEndOfFrame();
            _gameUIController.Initialize();

            while (_gameFlowController.IsLoadComplete == false)
                yield return new WaitForEndOfFrame();

            _gameFlowController.GenerateLevel();
            _stateMachine.Enter<GamePlayState>();
            yield return null;
        }
       
    }
}