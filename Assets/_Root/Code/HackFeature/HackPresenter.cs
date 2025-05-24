using System;
using _Root.Code.UpdateFeature;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Root.Code.HackFeature
{
    public class HackPresenter : IUpdatable
    {
        private readonly HackView _view;
        private readonly HackModel _model;
        private readonly HackFactory _factory;

        public HackPresenter(HackView view, HackModel model,  HackFactory factory)
        {
            _view = view;
            _model = model;
            _factory = factory;
            
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
        
        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0f)
                angle += 360f;
            return angle;
        }

        
        public void OnHackButtonPressed() 
        {
            var anyZone = false;
            var pointerAngle = NormalizeAngle(_view.pointer.eulerAngles.z);

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
            
        }

        public void Update(float deltaTime)
        {
            RotatePointer();
        }
    }
}