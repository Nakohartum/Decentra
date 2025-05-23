using UnityEngine;

namespace _Root.Code.MoveFeature
{
    public interface IMovable
    {
        void Move(Vector3 direction);
        void Rotate(Vector3 direction);
    }
}