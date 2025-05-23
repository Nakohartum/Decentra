using _Root.Code.MoveFeature;
using _Root.Code.UpdateFeature;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarPresenter 
    {
        private CarView _carView;
        private CarModel _carModel;
        private IMovable _movable;
        private bool _isInCar;

        public CarPresenter(CarView carView, CarModel carModel, IMovable movable)
        {
            _carView = carView;
            _carModel = carModel;
            _movable = movable;
            _isInCar = true;
        }
        
        public void Dispose()
        {
            
        }

        public void EnterVehicle()
        {
            _isInCar = true;
        }

        public void Move(Vector2 moveVector)
        {
            if (_isInCar)
            {
                _movable.Move(moveVector);
                _movable.Rotate(moveVector);
            }
        }
    }
}