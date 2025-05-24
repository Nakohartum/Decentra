using _Root.Code.InputFeature;
using _Root.Code.MoveFeature.CarMovement;
using Cinemachine;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarFactory
    {
        private InputController _inputController;
        private Transform[] _carTransforms;
        private int _currentCarIndex;

        public CarFactory(InputController inputController, Transform[] carTransforms)
        {
            _inputController = inputController;
            _carTransforms = carTransforms;
        }

        public CarPresenter CreateCar(CarSO carSo)
        {
            var view = Object.Instantiate(carSo.CarPrefab, _carTransforms[_currentCarIndex].position, _carTransforms[_currentCarIndex++].rotation);
            view.Rigidbody.mass = carSo.Mass;
            view.Rigidbody.drag = carSo.LinearDrag;
            view.Rigidbody.angularDrag = carSo.AngularDrag;
            var model = new CarModel(carSo.Speed, carSo.Health, carSo.Acceleration, carSo.TurnSpeed,
                carSo.SideFriction, carSo.CarSound);
            var movable = new PhysicsCarMovement(view.Rigidbody, model.Speed, model.Acceleration, model.TurnSpeed);
            var presenter = new CarPresenter(view, model, movable, _inputController);
            _inputController.OnSteeringWheelRotate.AddListener(presenter.GetInput);
            view.Initialize(presenter);
            return presenter;
        }
    }
}