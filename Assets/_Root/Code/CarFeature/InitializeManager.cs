using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class InitializeManager
    {
        private readonly CarSO _carSo;
        private readonly LevelView _levelPrefab;
        private readonly UpdateManager _updateManager;
        private readonly LevelFactory _levelFactory;
        private CinemachineTargetGroup _cinemachineTargetGroup;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, CinemachineTargetGroup cinemachineTargetGroup, LevelView levelPrefab)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _cinemachineTargetGroup = cinemachineTargetGroup;
            _levelPrefab = levelPrefab;
        }

        public void Initialize()
        {
            var inputController = new InputController();
            _updateManager.AddUpdatable(inputController);
            var levelFactory = new LevelFactory(_levelPrefab);
            var level = levelFactory.CreateLevel();
            var carFactory = new CarFactory(inputController, _cinemachineTargetGroup);
            
            var carPresenter = carFactory.CreateCar(_carSo, level.SpawnPosition.position, level.SpawnPosition.rotation);
        }
    }
}