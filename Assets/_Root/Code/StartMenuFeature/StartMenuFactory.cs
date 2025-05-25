namespace _Root.Code.StartMenuFeature
{
    public class StartMenuFactory
    {
        private StartMenuView _startMenuView;

        public StartMenuFactory(StartMenuView startMenuView)
        {
            _startMenuView = startMenuView;
        }

        public StartMenuPresenter CreatePresenter()
        {
            var presenter = new StartMenuPresenter(_startMenuView);
            _startMenuView.Button.onClick.AddListener(presenter.StartGame);
            return presenter;
        }
    }
}