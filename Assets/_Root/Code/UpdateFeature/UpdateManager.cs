using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Root.Code.UpdateFeature
{
    public class UpdateManager : IUpdatable, IFixedUpdate
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();
        public GameStatus GameStatus { get; private set; }
        private List<GameObject> _listToDestroy = new List<GameObject>();

        public void AddDestroyable(GameObject obj)
        {
            _listToDestroy.Add(obj);
        }

        public void Destroy()
        {
            foreach (var o in _listToDestroy)
            {
                Object.Destroy(o);
            }
            _listToDestroy.Clear();
        }

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
            if (GameStatus == GameStatus.GameEnded)
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
            if (GameStatus == GameStatus.GameEnded)
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
            GameStatus = status;
        }
    }
}