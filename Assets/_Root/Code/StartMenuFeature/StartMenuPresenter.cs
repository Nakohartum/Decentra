namespace _Root.Code.StartMenuFeature
{
    public class StartMenuPresenter
    {
        private StartMenuModel _model;
        private StartMenuView _view;
        public StartMenuPresenter(StartMenuModel model, StartMenuView view)
        {
            _model = model;
            _view = view;
        }

        public void StartGame()
        {
            _view.gameObject.SetActive(false);
        }

        public void ChangeViewsTitle()
        {
            _view.Title.text = _model.Title;
        }
    }
}