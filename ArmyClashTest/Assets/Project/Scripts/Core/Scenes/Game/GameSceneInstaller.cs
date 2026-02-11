using Project.Scripts.Gameplay;
using Project.Scripts.Gameplay.GameFlow;
using Project.Scripts.Gameplay.GameFlow.Logic;
using Project.Scripts.UI.Game;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Scenes.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private GameFlowController _gameFlowController;

        [SerializeField]
        private GameUIController _gameUIController;

        public override void InstallBindings()
        {
            BindSceneObjects();
            CreateSceneBootstrapper();
        }

        private void BindSceneObjects()
        {
            Container.Bind<IGameFlow>().FromInstance(_gameFlowController).AsSingle().NonLazy();
            Container.Bind<IGameUIController>().FromInstance(_gameUIController).AsSingle().NonLazy();

            Container.BindInterfacesTo<GameInfoService>().AsSingle().NonLazy();
        }

        private void CreateSceneBootstrapper()
        {
            GameSceneBootstrapper bootstrapper = Container.Instantiate<GameSceneBootstrapper>();
            bootstrapper.Initialize();
        }
    }
}