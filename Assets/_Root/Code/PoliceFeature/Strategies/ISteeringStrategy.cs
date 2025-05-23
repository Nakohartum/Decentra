using UnityEngine;

namespace _Root.Code.PoliceFeature.Strategies
{
    public interface ISteeringStrategy
    {
        Vector2 CalculateForce();
    }
}