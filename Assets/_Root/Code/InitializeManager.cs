using System.Collections;
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
        private Root _root;
        private Coroutine _policeSpawnRoutine;
        public InitializeManager(Root root)
        {
            _root = root;
            _root.LoseWinView.TryAgainButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            _root.UpdateManager.Destroy();
            StartGame();
        }

        public void Initialize()
        {
            var startMenuFactory = new StartMenuFactory(_root.StartMenuView);
            var startMenuPresenter = startMenuFactory.CreatePresenter();
            startMenuPresenter.OnStartClicked.AddListener(StartGame);
        }

        public void StartGame()
        {
            _root.UpdateManager.SetGameStatus(GameStatus.GameInProgress);
            var inputController = new InputController(_root.InputView);
            inputController.HideHackControllers();
            inputController.ShowPlayerControllers();
            _root.LoseWinView.gameObject.SetActive(false);
            _root.UpdateManager.AddUpdatable(inputController);
            var levelFactory = new LevelFactory(_root.LevelViewPrefab);
            var level = levelFactory.CreateLevel();
            level.GetWinTrigger().OnWin.AddListener(() =>
            {
                _root.LoseWinView.gameObject.SetActive(true);
                _root.LoseWinView.HideLoseScreen();
                _root.LoseWinView.ShowWinScreen();
                _root.UpdateManager.SetGameStatus(GameStatus.GameEnded);
            });
            _root.UpdateManager.AddDestroyable(level.gameObject);
            var playerFactory = new PlayerFactory(_root.PlayerSo, level.Spawnpoint.PlayerSpawnPoint, inputController);
            var playerPresenter = playerFactory.CreatePlayer();
            _root.UpdateManager.AddDestroyable(playerPresenter.PlayerView.gameObject);
            _root.CinemachineTargetGroup.AddMember(playerPresenter.PlayerView.transform, 1f, 10f);
            _root.UpdateManager.AddFixedUpdatable(playerPresenter);
            var carFactory = new CarFactory(inputController, level.Spawnpoint.CarsSpawnPoints);
            var policeFactory = new PoliceFactory(_root.PoliceSo, level.Spawnpoint.PoliceSpawnPoints);
            
            var hackFactory = new HackFactory(_root.InputView);
            var navigationPresenter = new NavigationPresenter(_root.NavigationView, level.GetWinTrigger().transform);
            SpawnCars(carFactory, policeFactory, playerPresenter, navigationPresenter);
            navigationPresenter.DisableArrow();
            _root.UpdateManager.AddUpdatable(navigationPresenter);
            
            inputController.OnActionButtonPressed.AddListener((_) =>
            {
                if (playerPresenter.CheckCarInDistance() == null)
                {
                    return;
                }
                inputController.HidePlayerControllers();
                var hackPresenter = hackFactory.CreateHackPresenter();
                inputController.ShowHackControllers();
                hackPresenter.OnMiniGameFinished.AddListener(() =>
                {
                    inputController.HidePlayerControllers();
                    inputController.HideHackControllers();
                    playerPresenter.EnterCar(true);
                    inputController.ShowHint();
                });
                _root.UpdateManager.AddUpdatable(hackPresenter);
            });
        }

        public void SpawnCars(CarFactory carFactory, PoliceFactory policeFactory,
            PlayerPresenter playerPresenter, NavigationPresenter navigationPresenter)
        {
            
            for (int i = 0; i < _root.CarSo.Length; i++)
            {
                var carPresenter = carFactory.CreateCar(_root.CarSo[i]);
                _root.UpdateManager.AddFixedUpdatable(carPresenter);
                _root.UpdateManager.AddDestroyable(carPresenter.CarView.gameObject);
                carPresenter.OnEnteredVehicle.AddListener(() =>
                {
                    _root.CinemachineTargetGroup.RemoveMember(playerPresenter.PlayerView.transform);
                    _root.CinemachineTargetGroup.AddMember(carPresenter.CarView.transform, 1f, 10f);
                    CreatePolice(policeFactory, carPresenter);
                    navigationPresenter.SetPlayer(carPresenter.CarView.transform);
                    navigationPresenter.EnableArrow();
                });
                carPresenter.OnVehicleDestroyed.AddListener(() =>
                {
                    _root.LoseWinView.gameObject.SetActive(true);
                    _root.LoseWinView.HideWinScreen();
                    _root.LoseWinView.ShowLoseScreen();
                    _root.UpdateManager.SetGameStatus(GameStatus.GameEnded);
                });
            }
        }

        private void CreatePolice(PoliceFactory policeFactory, CarPresenter carPresenter)
        {
            if (_policeSpawnRoutine != null)
            {
                _root.StopCoroutine(_policeSpawnRoutine);
            }
            _policeSpawnRoutine = _root.StartCoroutine(PoliceSpawnRoutine(policeFactory, carPresenter));
        }

        private IEnumerator PoliceSpawnRoutine(PoliceFactory policeFactory, CarPresenter carPresenter)
        {
            while (_root.MaxPoliceCount > policeFactory.CurrentPoliceCount)
            {
                for (float deltaTime = 0; deltaTime < _root.PoliceSpawnTimer; deltaTime+=Time.deltaTime)
                {
                    yield return null;
                }

                if (_root.UpdateManager.GameStatus == GameStatus.GameInProgress)
                {
                    var policePresenter = policeFactory.CreatePoliceUnit(carPresenter.CarView.Rigidbody);
                    _root.UpdateManager.AddDestroyable(policePresenter.PoliceView.gameObject);
                    _root.UpdateManager.AddUpdatable(policePresenter);
                }
                else
                {
                    break;
                }
            }
        }
    }
}