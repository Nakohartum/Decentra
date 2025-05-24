using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Root.Code.UpdateFeature
{
    public class UpdateManager : IUpdatable, IFixedUpdate
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();
        private GameStatus _gameStatus;

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }
        
        public void AddFixedUpdatable(IFixedUpdate updatable)
        {
            _fixedUpdates.Add(updatable);
        }

        public void RemoveFixedUpdatable(IFixedUpdate updatable)
        {
            _fixedUpdates.Remove(updatable);
        }
        
        public void Update(float deltaTime)
        {
            if (_gameStatus == GameStatus.GameEnded)
            {
                Dispose();
                return;
            }
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var fixedUpdate in _fixedUpdates)
            {
                fixedUpdate.Dispose();
            }
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Dispose();
            }
            _updatables.Clear();
            _fixedUpdates.Clear();
        }

        public void FixedUpdate()
        {
            if (_gameStatus == GameStatus.GameEnded)
            {
                Dispose();
                return;
            }
            foreach (IFixedUpdate fixedUpdate in _fixedUpdates)
            {
                fixedUpdate.FixedUpdate();
            }
        }

        public void SetGameStatus(GameStatus status)
        {
            _gameStatus = status;
        }
    }
}