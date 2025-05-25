using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceModel : IPoliceModel
    {
        public float MaxSpeed { get; }
        public float Acceleration { get; }
        public float AvoidForce { get; }
        public float AvoidDistance { get; }
        public float RamDistance { get; }
        public float RamMultiplier { get; }
        public LayerMask ObstacleLayers { get; }
        public AudioClip PoliceSound { get; }

        public PoliceModel(float maxSpeed, float acceleration, float avoidForce, float avoidDistance, float ramDistance, float ramMultiplier, LayerMask obstacleLayers,  AudioClip policeSound)
        {
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            AvoidForce = avoidForce;
            AvoidDistance = avoidDistance;
            RamDistance = ramDistance;
            RamMultiplier = ramMultiplier;
            ObstacleLayers = obstacleLayers;
            PoliceSound = policeSound;
        }
    }
}