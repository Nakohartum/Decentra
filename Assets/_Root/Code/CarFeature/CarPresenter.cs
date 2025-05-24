using System;
using _Root.Code.InputFeature;
using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using UnityEngine;

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

        public CarPresenter(CarView carView, CarModel carModel, IMovable movable, InputController inputController)
        {
            CarView = carView;
            _carModel = carModel;
            _movable = movable;
            _inputController = inputController;
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
            _inputController.HidePlayerControllers();
        }

        public void Move()
        {
            if (_isInCar)
            {
                Vector2 vector;
                
                _movable.Move(_inputVector);
                _movable.Rotate(_inputVector);
            }
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
    }
}