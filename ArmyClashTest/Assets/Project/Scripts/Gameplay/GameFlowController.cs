using System;
using Project.Scripts.Core.Events;
using Project.Scripts.Core.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Gameplay
{
    public interface IGameFlowController
    {
        bool IsLoadComplete { get; }

        void Initialize();
        void GenerateLevel();
        void SetGameOver(bool isWin);
        event Action<bool> OnGameOver;
    }

    public class GameFlowController : MonoBehaviour, IGameFlowController
    {
        [Inject] private GlobalEventProvider _globalEventProvider;
        [Inject] private IConfigsProvider _configsProvider;

        private bool _isGameComplete = false;
        private bool _isLoadComplete = false;

        public bool IsLoadComplete => _isLoadComplete;
        public event Action<bool> OnGameOver;

        public void Initialize()
        {
        }

        public void GenerateLevel()
        {
            _isGameComplete = false;
        }

        public void SetGameOver(bool isWin)
        {
            if (_isGameComplete)
                return;

            _isGameComplete = isWin;
            OnGameOver?.Invoke(isWin);
        }

        private void Start()
        {
            _isLoadComplete = true;
        }
    }
}