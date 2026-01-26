using Project.Scripts.Core.Events;
using Project.Scripts.Core.Infrastructure.Assets;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Core.Infrastructure.Data;
using Project.Scripts.Core.Infrastructure.ScenesManager;
using Project.Scripts.Core.Infrastructure.Services;
using Project.Scripts.Core.Infrastructure.StateMachines;
using Project.Scripts.Data;
using Project.Scripts.UI.LoadScreen;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Scenes.Core
{
    public class GameCoreInstaller : MonoInstaller
    {
        [SerializeField]
        private GameCoreBootstrapper _gameCoreBootstrapper;

        [SerializeField]
        private LoadingPanel _loadingPanel;

        public override void InstallBindings()
        {
            BindProviders();
            BindDatabase();

            BindServices();

            BindFactories();
            BindGameStateMachine();

            BindGameBootstrapper();
            BindSceneLoader();
        }

        private void BindProviders()
        {
            Container.BindInterfacesTo<AssetProvider>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ConfigsProvider>().AsSingle().NonLazy();
            Container.Bind<GlobalEventProvider>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<LevelsDataService>().AsSingle().NonLazy();
        }

        private void BindDatabase()
        {
            IDatabase database = new GameDatabase();
            Container.BindInstance(database).AsSingle().NonLazy();

#if UNITY_EDITOR
            DataEditorMediator.SetDatabase(database);
#endif
        }

        private void BindFactories()
        {
            Container.Bind<StateMachineFactory>().AsSingle().NonLazy();
        }

        private void BindGameStateMachine() =>
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();

        private void BindGameBootstrapper() =>
            Container.Bind<ICoroutineRunner>().FromInstance(_gameCoreBootstrapper).AsSingle().NonLazy();

        private void BindSceneLoader()
        {
            Container.Bind<LoadingPanel>().FromInstance(_loadingPanel).AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoader>().AsSingle().NonLazy();
        }
    }
}