using System;
using System.Collections.Generic;

namespace _Root.Code.UpdateFeature
{
    public class UpdateManager : IUpdatable
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }
        
        public void Update(float deltaTime)
        {
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Dispose();
            }
            _updatables.Clear();
        }
    }
}