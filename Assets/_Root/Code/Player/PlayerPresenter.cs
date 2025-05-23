using _Root.Code;
using _Root.Code.MoveFeature;
using Player;
using UnityEngine;

namespace Player
{
    public class PlayerPresenter
    {
        private PlayerModel _playerModel;
        private PlayerView _view;
        private IMovable _movable;

        public PlayerPresenter(PlayerModel playerModel, PlayerView view, IMovable movable)
        {
            _playerModel = playerModel;
            _view = view;
            _movable = movable;
        }

        public void Move(Vector2 arg0)
        {
            _movable.Move(arg0);
        }
    }
}