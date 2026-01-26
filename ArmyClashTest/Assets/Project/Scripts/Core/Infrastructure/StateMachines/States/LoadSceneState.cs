using System;
using Project.Scripts.Core.Infrastructure.ScenesManager;

namespace Project.Scripts.Core.Infrastructure.StateMachines.States
{
    public abstract class LoadSceneState
    {
        private readonly ISceneLoader _sceneLoader;

        protected LoadSceneState(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        protected void Enter(string scene, Action onLoaded) =>
            _sceneLoader.Load(scene, onLoaded);

        protected void Enter(Enums.Scenes scene, Action onLoaded) =>
            _sceneLoader.Load(scene, onLoaded);
    }
}