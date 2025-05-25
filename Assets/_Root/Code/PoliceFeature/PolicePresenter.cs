using System.Collections.Generic;
using _Root.Code.MoveFeature;
using _Root.Code.PoliceFeature.Strategies;
using _Root.Code.UpdateFeature;
using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PolicePresenter : IUpdatable
    {
        public PoliceView PoliceView { get; private set; }
        private readonly PoliceModel _policeModel;
        private IMovable _movable;
        private PoliceSoundPlayer _soundPlayer;

        public PolicePresenter(PoliceView policeView, PoliceModel policeModel, IMovable movable, PoliceSoundPlayer soundPlayer)
        {
            PoliceView = policeView;
            _policeModel = policeModel;
            _movable = movable;
            _soundPlayer = soundPlayer;
            if (_soundPlayer != null)
            {
                PoliceView.OnSoundPlay += _soundPlayer.PlaySound;
            }
        }
        
        public void Dispose()
        {
            PoliceView.Rigidbody2D.velocity = Vector2.zero;
        }

        public void Update(float deltaTime)
        {
            _movable.Move(Vector2.zero);
            _movable.Rotate(Vector2.zero);
        }

        public void PlaySound()
        {
            PoliceView.PlaySound();
        }
    }
}