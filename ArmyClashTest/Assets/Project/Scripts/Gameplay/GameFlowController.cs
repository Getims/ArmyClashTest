using System;
using Project.Scripts.Core.Events;
using Project.Scripts.Core.Infrastructure.Configs;
using Project.Scripts.Gameplay.Factory;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay
{
    public interface IGameFlowController
    {
        bool IsLoadComplete { get; }

        void Initialize();
        void GenerateUnits();
        void StartBattle();
        void SetGameOver();
        event Action OnGameOver;
    }

    public class GameFlowController : MonoBehaviour, IGameFlowController
    {
        [SerializeField]
        private UnitsController _unitsController;

        [SerializeField]
        private UnitsFactory _unitsFactory;

        [Inject] private GlobalEventProvider _globalEventProvider;
        [Inject] private IConfigsProvider _configsProvider;
        [Inject] private IGameInfoService _gameInfoService;

        private bool _isGameComplete = false;
        private bool _isLoadComplete = false;

        public bool IsLoadComplete => _isLoadComplete;
        public event Action OnGameOver;

        public void Initialize()
        {
            _unitsFactory.Initialize();
            _unitsController.Initialize(_unitsFactory);
            _isGameComplete = false;
        }

        public void GenerateUnits()
        {
            _unitsController.CreateUnits();
        }

        public void StartBattle()
        {
            _unitsController.StartBattle();
        }

        public void SetGameOver()
        {
            if (_isGameComplete)
                return;

            _unitsController.StopBattle();
            _isGameComplete = true;
            OnGameOver?.Invoke();
        }

        private void Start()
        {
            _isLoadComplete = true;
            _gameInfoService.OnOneTeamAlive += OnOneTeamAlive;
        }

        private void OnDestroy()
        {
            _gameInfoService.OnOneTeamAlive -= OnOneTeamAlive;
        }

        private void OnOneTeamAlive() => SetGameOver();
    }
}