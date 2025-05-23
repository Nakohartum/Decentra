using UnityEngine;

namespace _Root.Code.PoliceFeature.Strategies
{
    public class PursueSteeringStrategy : ISteeringStrategy
    {
        private readonly Transform _agent;
        private readonly Transform _target;
        private readonly Rigidbody2D _targetRb;
        private readonly float _acceleration;

        public PursueSteeringStrategy(Transform agent, Transform target, Rigidbody2D targetRb, float acceleration)
        {
            _agent = agent;
            _target = target;
            _targetRb = targetRb;
            _acceleration = acceleration;
        }

        public Vector2 CalculateForce()
        {
            Vector2 prediction = (Vector2)_target.position + _targetRb.velocity * 0.5f;
            Vector2 dir = (prediction - (Vector2)_agent.position).normalized;
            return dir * _acceleration;
        }
    }
}