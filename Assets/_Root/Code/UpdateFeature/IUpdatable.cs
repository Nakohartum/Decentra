using System;

namespace _Root.Code.UpdateFeature
{
    public interface IUpdatable : IDisposable
    {
        void Update(float deltaTime);
    }
}