using UnityEngine.Events;

namespace _Root.Code.StartMenuFeature
{
    public class StartMenuPresenter
    {
        private StartMenuView _view;
        public UnityEvent OnStartClicked { get; private set; }
        public StartMenuPresenter(StartMenuView view)
        {
            _view = view;
            OnStartClicked = new UnityEvent();
        }

        public void StartGame()
        {
            OnStartClicked.Invoke();
            _view.gameObject.SetActive(false);
        }
    }
}