using _Root.Code.InputFeature;
using _Root.Code.MoveFeature.CarMovement;
using Cinemachine;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarFactory
    {
        private InputController _inputController;
        private CinemachineTargetGroup _cinemachineTargetGroup;

        public CarFactory(InputController inputController, CinemachineTargetGroup cinemachineTargetGroup)
        {
            _inputController = inputController;
            _cinemachineTargetGroup = cinemachineTargetGroup;
        }

        public CarPresenter CreateCar(CarSO carSo, Vector3 position, Quaternion rotation)
        {
            var view = Object.Instantiate(carSo.CarPrefab, position, rotation);
            view.Rigidbody.mass = carSo.Mass;
            view.Rigidbody.drag = carSo.LinearDrag;
            view.Rigidbody.angularDrag = carSo.AngularDrag;
            var model = new CarModel(carSo.Speed, carSo.Health, carSo.Acceleration, carSo.TurnSpeed,
                carSo.SideFriction);
            var movable = new PhysicsCarMovement(view.Rigidbody, model.Speed, model.Acceleration, model.TurnSpeed);
            var presenter = new CarPresenter(view, model, movable, _inputController);
            _inputController.OnSteeringWheelRotate.AddListener(presenter.GetInput);
            _cinemachineTargetGroup.AddMember(view.transform, 1f, 10f);
            view.Initialize(presenter);
            return presenter;
        }
    }
}