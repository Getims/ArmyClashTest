using Project.Scripts.Core.Enums;
using Project.Scripts.Gameplay;
using Project.Scripts.Gameplay.GameFlow.Logic;
using Project.Scripts.UI.Common.Panels;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI.Game
{
    public class LevelCompletePanel : PopupPanel
    {
        [SerializeField]
        private TMP_Text _winText;

        [Inject] private IGameInfoService _gameInfoService;

        public override void Show()
        {
            base.Show();
            UpdateWinText();
        }

        public override void Hide()
        {
            base.Hide();
        }

        protected override void OnClaimButtonClick()
        {
            base.OnClaimButtonClick();
            Hide();
        }

        private void UpdateWinText()
        {
            UnitTeam winner = UnitTeam.Team1;

            foreach (var teamInfo in _gameInfoService.UnitsDictionary)
            {
                if (teamInfo.Value > 0)
                {
                    winner = teamInfo.Key;
                    break;
                }
            }

            _winText.text = $"Team {winner} won the battle!";
        }
    }
}