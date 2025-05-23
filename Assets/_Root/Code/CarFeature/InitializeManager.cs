using _Root.Code.InputFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class InitializeManager
    {
        private readonly CarSO _carSo;
        private readonly UpdateManager _updateManager;
        private readonly Transform _carSpawnPosition;
        private CinemachineTargetGroup _cinemachineTargetGroup;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, Transform carSpawnPosition, CinemachineTargetGroup cinemachineTargetGroup)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _carSpawnPosition = carSpawnPosition;
            _cinemachineTargetGroup = cinemachineTargetGroup;
        }

        public void Initialize()
        {
            var inputController = new InputController();
            _updateManager.AddUpdatable(inputController);
            var carFactory = new CarFactory(inputController, _cinemachineTargetGroup);
            var carPresenter = carFactory.CreateCar(_carSo, _carSpawnPosition.position, _carSpawnPosition.rotation);
        }
    }
}