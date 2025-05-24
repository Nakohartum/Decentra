using System;
using _Root.Code.UpdateFeature;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Root.Code.HackFeature
{
    public class HackPresenter : IUpdatable
    {
        private readonly HackView _view;
        private readonly HackModel _model;
        private readonly HackFactory _factory;
        public UnityEvent OnMiniGameFinished;

        public HackPresenter(HackView view, HackModel model,  HackFactory factory)
        {
            _view = view;
            _model = model;
            _factory = factory;
            OnMiniGameFinished = new UnityEvent();
        }
        
        

        private void OnPlayPressed(bool inZone)
        {
            var isComplete = _model.IsHitSuccessful(inZone);

            switch (isComplete)
            {
                case HackStatus.InProgress:
                    _view.ShowSuccess(_model.CurrentHits,  _model.RequiredHits);
                    break;
                case HackStatus.Completed:
                    _view.ShowHackComplete();
                    _view.gameObject.SetActive(false);
                    OnMiniGameFinished.Invoke();
                    Dispose();
                    break;
                case HackStatus.Failed:
                    _view.ShowFailure();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RotatePointer()
        {
            _view.pointer.Rotate(0, 0, -180f * Time.deltaTime);
        }

        public void Reset()
        {
            _model.Reset();
        }

        
        public void OnHackButtonPressed() 
        {
            var anyZone = false;

            foreach (var zone in _view.successZones)
            {
                float delta = Mathf.DeltaAngle(_view.pointer.eulerAngles.z, zone.eulerAngles.z);
                if (Mathf.Abs(delta) < 40f)
                {
                    anyZone = true;
                    _factory.CreateRandomSuccessZone(_view);
                    break;
                }
            }
            OnPlayPressed(anyZone);
        }

        
        public void Dispose()
        {
            OnMiniGameFinished.RemoveAllListeners();
        }

        public void Update(float deltaTime)
        {
            RotatePointer();
        }
    }
}