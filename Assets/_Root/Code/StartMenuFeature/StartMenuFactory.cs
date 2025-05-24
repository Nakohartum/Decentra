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
            var presenter = new StartMenuPresenter(new StartMenuModel("Hijack in 30 seconds"), _startMenuView);
            _startMenuView.Button.onClick.AddListener(presenter.StartGame);
            presenter.ChangeViewsTitle();
            return presenter;
        }
    }
}