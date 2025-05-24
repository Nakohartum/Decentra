using System;
using _Root.Code;
using _Root.Code.CarFeature;
using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using Player;
using UnityEngine;

namespace Player
{
    public class PlayerPresenter : IDisposable, IFixedUpdate
    {
        private PlayerModel _playerModel;
        private PlayerView _view;
        private IMovable _movable;
        private Vector2 _inputVector;
        public PlayerPresenter(PlayerModel playerModel, PlayerView view, IMovable movable)
        {
            _playerModel = playerModel;
            _view = view;
            _movable = movable;
        }

        public void GetInputVector(Vector2 inputVector)
        {
            _inputVector.Set(inputVector.x, inputVector.y);
        }

        public void Move(Vector2 arg0)
        {
            _movable.Move(_inputVector);
            _movable.Rotate(_inputVector);
        }

        public void EnterCar(bool value)
        {
            if (value)
            {
                var hit = Physics2D.RaycastAll(_view.transform.position, _view.transform.up, 1f);
                
                if (hit.Length > 0)
                {
                    CarView carView;
                    foreach (var hit2D in hit)
                    {
                        if (hit2D.collider.TryGetComponent<CarView>(out carView))
                        {
                            carView.CarPresenter.EnterVehicle();
                            Dispose();
                            break;
                        }
                    }
                    
                }
            }
        }

        public void Dispose()
        {
            _view.gameObject.SetActive(false);
        }

        public void FixedUpdate()
        {
            Move(_inputVector);
        }
    }
}