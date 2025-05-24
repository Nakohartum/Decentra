using System.Collections.Generic;
using _Root.Code.CarFeature;
using _Root.Code.GameLoseWinFeature;
using _Root.Code.HackFeature;
using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.NavigationFeature;
using _Root.Code.Player;
using _Root.Code.PoliceFeature;
using _Root.Code.StartMenuFeature;
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
        private LoseWinView _loseWinView;
        private List<GameObject> _listToDestroy = new List<GameObject>();
        private StartMenuView _startMenuView;
        private NavigationView _navigationView;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, 
            CinemachineTargetGroup cinemachineTargetGroup, LevelView levelPrefab, PoliceSO policeSo, PlayerSO playerSo, InputView inputView, LoseWinView loseWinView, StartMenuView startMenuView, NavigationView navigationView)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _cinemachineTargetGroup = cinemachineTargetGroup;
            _levelPrefab = levelPrefab;
            _policeSo = policeSo;
            _playerSo = playerSo;
            _inputView = inputView;
            _loseWinView = loseWinView;
            _startMenuView = startMenuView;
            _navigationView = navigationView;
            _loseWinView.TryAgainButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            foreach (var o in _listToDestroy)
            {
                Object.Destroy(o);
            }
            _listToDestroy.Clear();
            StartGame();
        }

        public void Initialize()
        {
            var startMenuFactory = new StartMenuFactory(_startMenuView);
            var startMenuPresenter = startMenuFactory.CreatePresenter();
            startMenuPresenter.OnStartClicked.AddListener(StartGame);
        }

        public void StartGame()
        {
            _updateManager.SetGameStatus(GameStatus.GameInProgress);
            var inputController = new InputController(_inputView);
            inputController.HideHackControllers();
            inputController.ShowPlayerControllers();
            _loseWinView.gameObject.SetActive(false);
            _updateManager.AddUpdatable(inputController);
            var levelFactory = new LevelFactory(_levelPrefab);
            var level = levelFactory.CreateLevel();
            level.WinTrigger.OnWin.AddListener(() =>
            {
                _loseWinView.gameObject.SetActive(true);
                _loseWinView.HideLoseScreen();
                _loseWinView.ShowWinScreen();
                _updateManager.SetGameStatus(GameStatus.GameEnded);
            });
            var playerFactory = new PlayerFactory(_playerSo, level.SpawnPosition, inputController);
            var playerPresenter = playerFactory.CreatePlayer();
            _updateManager.AddFixedUpdatable(playerPresenter);
            var carFactory = new CarFactory(inputController, _cinemachineTargetGroup);
            var carPresenter = carFactory.CreateCar(_carSo, level.SpawnPosition.position, level.SpawnPosition.rotation);
            _updateManager.AddFixedUpdatable(carPresenter);
            var policeFactory = new PoliceFactory(_policeSo);
            var hackFactory = new HackFactory(_inputView);
            var navigationPresenter = new NavigationPresenter(_navigationView, level.WinTrigger.transform,
                carPresenter.CarView.transform);
            navigationPresenter.DisableArrow();
            _updateManager.AddUpdatable(navigationPresenter);
            carPresenter.OnEnteredVehicle.AddListener(() =>
            {
                var policePresenter = policeFactory.CreatePoliceUnit(carPresenter.CarView.Rigidbody);
                _updateManager.AddUpdatable(policePresenter);
                _listToDestroy.Add(policePresenter.PoliceView.gameObject);
                navigationPresenter.EnableArrow();
            });
            carPresenter.OnVehicleDestroyed.AddListener(() =>
            {
                _loseWinView.gameObject.SetActive(true);
                _loseWinView.HideWinScreen();
                _loseWinView.ShowLoseScreen();
                _updateManager.SetGameStatus(GameStatus.GameEnded);
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
            
            _listToDestroy.Add(playerPresenter.PlayerView.gameObject);
            _listToDestroy.Add(carPresenter.CarView.gameObject);
            _listToDestroy.Add(level.gameObject);
        }
    }
}