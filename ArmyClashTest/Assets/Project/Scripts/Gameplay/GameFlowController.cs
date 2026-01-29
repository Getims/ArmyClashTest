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
        void SetGameOver(bool isWin);
        event Action<bool> OnGameOver;
    }

    public class GameFlowController : MonoBehaviour, IGameFlowController
    {
        [SerializeField]
        private UnitsController _unitsController;

        [SerializeField]
        private UnitsFactory _unitsFactory;

        [Inject] private GlobalEventProvider _globalEventProvider;
        [Inject] private IConfigsProvider _configsProvider;

        private bool _isGameComplete = false;
        private bool _isLoadComplete = false;

        public bool IsLoadComplete => _isLoadComplete;
        public event Action<bool> OnGameOver;

        public void Initialize()
        {
            _unitsFactory.Initialize();
            _unitsController.Initialize(_unitsFactory);
            _unitsController.OnOneTeamAlive += OnOneTeamAlive;
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

        public void SetGameOver(bool isWin)
        {
            if (_isGameComplete)
                return;

            //_unitsController.ClearUnits();
            _isGameComplete = isWin;
            OnGameOver?.Invoke(isWin);
        }

        private void Start()
        {
            _isLoadComplete = true;
        }

        private void OnOneTeamAlive()
        {
            SetGameOver(true);
        }
    }
}