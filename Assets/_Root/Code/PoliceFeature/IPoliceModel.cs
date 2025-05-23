using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public interface IPoliceModel
    {
        float MaxSpeed { get; }
        float Acceleration { get; }
        float AvoidForce { get; }
        float AvoidDistance { get; }
        float RamDistance { get; }
        float RamMultiplier { get; }
        LayerMask ObstacleLayers { get; }
    }
}