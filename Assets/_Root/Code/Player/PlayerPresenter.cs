using System;
using _Root.Code;
using _Root.Code.CarFeature;
using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using Player;
using UnityEngine;

namespace Player
{
    public class PlayerPresenter : IFixedUpdate
    {
        private PlayerModel _playerModel;
        public PlayerView PlayerView {get; private set;}
        private IMovable _movable;
        private Vector2 _inputVector;
        private int _isWalkingHash = Animator.StringToHash("IsWalking");
        public PlayerPresenter(PlayerModel playerModel, PlayerView playerView, IMovable movable)
        {
            _playerModel = playerModel;
            PlayerView = playerView;
            _movable = movable;
        }

        public void GetInputVector(Vector2 inputVector)
        {
            _inputVector.Set(inputVector.x, inputVector.y);
        }

        public void Move(Vector2 arg0)
        {
            PlayerView.Animator.SetBool(_isWalkingHash, arg0 != Vector2.zero);
            _movable.Move(arg0);
            _movable.Rotate(arg0);
        }

        public void EnterCar(bool value)
        {
            if (value)
            {
                var carView = CheckCarInDistance();
                carView.CarPresenter.EnterVehicle();
                Dispose();
            }
        }

        public CarView CheckCarInDistance()
        {
            var hit = Physics2D.RaycastAll(PlayerView.transform.position, PlayerView.transform.up, 1f);
                
            if (hit.Length > 0)
            {
                CarView carView;
                foreach (var hit2D in hit)
                {
                    if (hit2D.collider.TryGetComponent<CarView>(out carView))
                    {
                        return carView;
                    }
                }
                    
            }

            return null;
        }

        public void Dispose()
        {
            PlayerView.Rigidbody.velocity = Vector2.zero;
            PlayerView.gameObject.SetActive(false);
        }

        public void FixedUpdate()
        {
            Move(_inputVector);
        }
    }
}