using Project.Scripts.UI.Common.Panels;

namespace Project.Scripts.UI.Game
{
    public class LevelCompletePanel : PopupPanel
    {
        public override void Show()
        {
            base.Show();
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
    }
}