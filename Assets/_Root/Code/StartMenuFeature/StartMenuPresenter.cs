using UnityEngine.Events;

namespace _Root.Code.StartMenuFeature
{
    public class StartMenuPresenter
    {
        private StartMenuModel _model;
        private StartMenuView _view;
        public UnityEvent OnStartClicked { get; private set; }
        public StartMenuPresenter(StartMenuModel model, StartMenuView view)
        {
            _model = model;
            _view = view;
            OnStartClicked = new UnityEvent();
        }

        public void StartGame()
        {
            OnStartClicked.Invoke();
            _view.gameObject.SetActive(false);
        }

        public void ChangeViewsTitle()
        {
            _view.Title.text = _model.Title;
        }
    }
}