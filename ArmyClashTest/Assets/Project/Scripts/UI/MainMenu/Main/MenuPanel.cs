using System;
using Project.Scripts.Data;
using Project.Scripts.UI.Common.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI.MainMenu.Main
{
    public class MenuPanel : UIPanel
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private TMP_Text _battlesCount;

        [SerializeField]
        private TeamInfo _teamInfoPrefab;

        [SerializeField]
        private Transform _container;

        [Inject] private IGameDataService _gameDataService;

        public event Action OnStartLevelOpenRequest;

        public override void Show()
        {
            base.Show();
            UpdateBattlesCount();
            UpdateTeamsInfo();
        }

        protected void Start()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        protected override void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void UpdateBattlesCount()
        {
            _battlesCount.text = $"Battles count: {_gameDataService.BattlesCount.Value}";
        }

        private void UpdateTeamsInfo()
        {
            var teamsInfo = _gameDataService.TeamsInfo;

            foreach (var teamInfo in teamsInfo)
            {
                var newTeamInfo = Instantiate(_teamInfoPrefab, _container);
                newTeamInfo.UpdateInfo(teamInfo.UnitTeam, teamInfo.WinsCount);
            }
        }

        private void OnStartButtonClick()
        {
            OnStartLevelOpenRequest?.Invoke();
        }
    }
}