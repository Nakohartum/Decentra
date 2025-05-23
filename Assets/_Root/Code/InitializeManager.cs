using _Root.Code.CarFeature;
using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.PoliceFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using UnityEngine;

namespace _Root.Code
{
    public class InitializeManager
    {
        private readonly CarSO _carSo;
        private readonly LevelView _levelPrefab;
        private readonly UpdateManager _updateManager;
        private readonly LevelFactory _levelFactory;
        private CinemachineTargetGroup _cinemachineTargetGroup;
        private PoliceSO _policeSo;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, 
            CinemachineTargetGroup cinemachineTargetGroup, LevelView levelPrefab, PoliceSO policeSo)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _cinemachineTargetGroup = cinemachineTargetGroup;
            _levelPrefab = levelPrefab;
            _policeSo = policeSo;
        }

        public void Initialize()
        {
            var inputController = new InputController();
            _updateManager.AddUpdatable(inputController);
            var levelFactory = new LevelFactory(_levelPrefab);
            var level = levelFactory.CreateLevel();
            var carFactory = new CarFactory(inputController, _cinemachineTargetGroup);
            
            var carPresenter = carFactory.CreateCar(_carSo, level.SpawnPosition.position, level.SpawnPosition.rotation);

            var policeFactory = new PoliceFactory(_policeSo.PolicePrefab, _policeSo, carPresenter.CarView.Rigidbody);
            var policePresenter = policeFactory.CreatePoliceUnit();
            _updateManager.AddUpdatable(policePresenter);
        }
    }
}