using System;
using Project.Scripts.UI.Common.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Game
{
    public class TopGamePanel : UIPanel
    {
        [SerializeField]
        private Button _settingsButton;

        public event Action OnSettingsClick;

        protected void Start()
        {
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        protected override void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick() => OnSettingsClick?.Invoke();
    }
}