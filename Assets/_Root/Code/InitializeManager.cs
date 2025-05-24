using _Root.Code.CarFeature;
using _Root.Code.HackFeature;
using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.Player;
using _Root.Code.PoliceFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using Player;
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
        private PlayerSO _playerSo;
        private InputView _inputView;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, 
            CinemachineTargetGroup cinemachineTargetGroup, LevelView levelPrefab, PoliceSO policeSo, PlayerSO playerSo, InputView inputView)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _cinemachineTargetGroup = cinemachineTargetGroup;
            _levelPrefab = levelPrefab;
            _policeSo = policeSo;
            _playerSo = playerSo;
            _inputView = inputView;
        }

        public void Initialize()
        {
            var inputController = new InputController(_inputView);
            inputController.HideHackControllers();
            inputController.ShowPlayerControllers();
            _updateManager.AddUpdatable(inputController);
            var levelFactory = new LevelFactory(_levelPrefab);
            var level = levelFactory.CreateLevel();
            var playerFactory = new PlayerFactory(_playerSo, level.SpawnPosition, inputController);
            var playerPresenter = playerFactory.CreatePlayer();
            _updateManager.AddFixedUpdatable(playerPresenter);
            var carFactory = new CarFactory(inputController, _cinemachineTargetGroup);
            var carPresenter = carFactory.CreateCar(_carSo, level.SpawnPosition.position, level.SpawnPosition.rotation);
            _updateManager.AddFixedUpdatable(carPresenter);
            var policeFactory = new PoliceFactory(_policeSo);
            var hackFactory = new HackFactory(_inputView);
            carPresenter.OnEnteredVehicle.AddListener(() =>
            {
                var policePresenter = policeFactory.CreatePoliceUnit(carPresenter.CarView.Rigidbody);
                _updateManager.AddUpdatable(policePresenter);
            });
            inputController.OnActionButtonPressed.AddListener((_) =>
            {
                inputController.HidePlayerControllers();
                var hackPresenter = hackFactory.CreateHackPresenter();
                inputController.ShowHackControllers();
                hackPresenter.OnMiniGameFinished.AddListener(() =>
                {
                    inputController.HidePlayerControllers();
                    inputController.HideHackControllers();
                    playerPresenter.EnterCar(true);
                });
                _updateManager.AddUpdatable(hackPresenter);
            });
        }
    }
}