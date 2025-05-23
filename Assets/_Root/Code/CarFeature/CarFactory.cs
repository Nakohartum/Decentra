using _Root.Code.InputFeature;
using _Root.Code.MoveFeature.CarMovement;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarFactory
    {
        private InputController _inputController;

        public CarFactory(InputController inputController)
        {
            _inputController = inputController;
        }

        public CarPresenter CreateCar(CarSO carSo, Vector3 position, Quaternion rotation)
        {
            var view = Object.Instantiate(carSo.CarPrefab, position, rotation);
            var model = new CarModel(carSo.Speed, carSo.Health, carSo.Acceleration, carSo.TurnSpeed);
            var movable = new PhysicsCarMovement(view.Rigidbody, model.Speed, model.Acceleration, model.TurnSpeed);
            var presenter = new CarPresenter(view, model, movable);
            _inputController.OnMove.AddListener(presenter.Move);
            return presenter;
        }
    }
}