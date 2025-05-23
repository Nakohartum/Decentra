using System;
using _Root.Code.InputFeature;
using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarPresenter 
    {
        public CarView CarView { get; private set; }
        private CarModel _carModel;
        private IMovable _movable;
        private bool _isInCar = false;
        private InputController _inputController;

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

        public void EnterVehicle()
        {
            _isInCar = true;
            CarView.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _inputController.HidePlayerControllers();
        }

        public void Move(TouchSide touchSide)
        {
            if (_isInCar)
            {
                Vector2 vector;
                switch (touchSide)
                {
                    case TouchSide.None:
                        vector = new Vector2(0, 1);
                        break;
                    case TouchSide.Left:
                        vector = new Vector2(-1, 1);
                        break;
                    case TouchSide.Right:
                        vector = new Vector2(1, 1);
                        break;
                    case TouchSide.Center:
                        vector = new Vector2(0, -1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(touchSide), touchSide, null);
                }
                _movable.Move(vector);
                _movable.Rotate(vector);
            }
        }
    }
}