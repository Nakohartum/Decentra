using _Root.Code;
using _Root.Code.InputFeature;
using _Root.Code.Player;
using UnityEngine;

namespace Player
{
    public class PlayerFactory
    {
        private PlayerSO _playerSo;
        private Transform _playerSpawnPoint;
        private InputController _inputController;

        public PlayerFactory(PlayerSO playerSo, Transform playerSpawnPoint, InputController inputController)
        {
            _playerSo = playerSo;
            _playerSpawnPoint = playerSpawnPoint;
            _inputController = inputController;
        }

        public PlayerPresenter CreatePlayer()
        {
            var view = Object.Instantiate(_playerSo.PlayerViewPrefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation);

            var model = new PlayerModel(_playerSo.Speed);
            var playerMovement = new PlayerMovement(view.Rigidbody, model.Speed);
            var presenter = new PlayerPresenter(model, view, playerMovement);
            _inputController.OnMove.AddListener(presenter.GetInputVector);
            return presenter;
        }
    }
}