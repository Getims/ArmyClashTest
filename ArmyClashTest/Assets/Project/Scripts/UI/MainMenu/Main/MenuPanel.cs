using System;
using Project.Scripts.UI.Common.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.MainMenu.Main
{
    public class MenuPanel : UIPanel
    {
        [SerializeField]
        private Button _startButton;

        public event Action OnStartLevelOpenRequest;

        protected void Start()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
        }

        protected override void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            OnStartLevelOpenRequest?.Invoke();
        }
    }
}