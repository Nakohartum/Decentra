namespace _Root.Code.HackFeature.HackPresenter
{
    public class HackPresenter
    {
        private HackView.HackView _view;
        private HackModel.HackModel _model;

        public HackPresenter(HackView.HackView view, HackModel.HackModel model)
        {
            _view = view;
            _model = model;
        }

        public void OnPlayPressed(bool inZone)
        {
            var isComplete = _model.IsHitSuccessful(inZone);

            if (!isComplete)
            {
                _view.ShowFailure();
            }
            
            if (isComplete)
                _view.ShowHackComplete();
            
            else
                _view.ShowSuccess(_model.CurrentHits, _model.RequiredHits);
            
        }

        public void Reset()
        {
            _model.Reset();
        }
    }
}