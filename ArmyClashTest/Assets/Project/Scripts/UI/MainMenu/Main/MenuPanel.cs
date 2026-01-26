using System;
using Project.Scripts.UI.Common.Panels;

namespace Project.Scripts.UI.MainMenu.Main
{
    public class MenuPanel : UIPanel
    {
        public event Action OnStartLevelOpenRequest;

        private void Start()
        {
        }

        private void OnControlButtonClick()
        {
            OnStartLevelOpenRequest?.Invoke();
        }
    }
}