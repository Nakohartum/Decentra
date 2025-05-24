using System;
using _Root.Code.InputFeature;
using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace _Root.Code.CarFeature
{
    public class CarPresenter : IFixedUpdate
    {
        public CarView CarView { get; private set; }
        private CarModel _carModel;
        private IMovable _movable;
        private bool _isInCar = false;
        private InputController _inputController;
        private Vector2 _inputVector;
        public UnityEvent OnEnteredVehicle {get; private set;}

        public CarPresenter(CarView carView, CarModel carModel, IMovable movable, InputController inputController)
        {
            CarView = carView;
            _carModel = carModel;
            _movable = movable;
            _inputController = inputController;
            OnEnteredVehicle = new UnityEvent();
        }
        
        public void Dispose()
        {
            
        }

        public void FixedUpdate()
        {
            Move();
        }

        public void EnterVehicle()
        {
            _isInCar = true;
            CarView.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            OnEnteredVehicle.Invoke();
        }

        public void Move()
        {
            if (_isInCar)
            {
                _movable.Move(_inputVector);
                KillSideVelocity();
                _movable.Rotate(_inputVector);
            }
        }

        private void KillSideVelocity()
        {
            var velocity = CarView.Rigidbody.velocity;
            Vector2 forward = CarView.transform.up * Vector2.Dot(velocity, CarView.transform.up);
            Vector2 side = CarView.transform.right * Vector2.Dot(velocity, CarView.transform.right);
            CarView.Rigidbody.velocity = forward + side * _carModel.SideFriction;
        }

        public void GetInput(TouchSide value)
        {
            switch (value)
            {
                case TouchSide.None:
                    _inputVector = new Vector2(0, 1);
                    break;
                case TouchSide.Left:
                    _inputVector = new Vector2(-1, 1);
                    break;
                case TouchSide.Right:
                    _inputVector = new Vector2(1, 1);
                    break;
                case TouchSide.Center:
                    _inputVector = new Vector2(0, -1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void GetDamage()
        {
            _carModel.ApplyDamage(GameSettings.DAMAGE);
            if (_carModel.Health <= 0)
            {
                Object.Destroy(CarView.gameObject);
            }
        }
    }
}