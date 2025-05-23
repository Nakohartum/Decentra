using UnityEngine;

namespace _Root.Code.PoliceFeature.Strategies
{
    public class RamSteeringStrategy : ISteeringStrategy
    {
        private readonly Transform _agent;
        private readonly Transform _target;
        private readonly float _acceleration;
        private readonly float _ramDistance;
        private readonly float _ramMultiplier;

        public RamSteeringStrategy(Transform agent, Transform target, float acceleration, float ramDistance, float ramMultiplier)
        {
            _agent = agent;
            _target = target;
            _acceleration = acceleration;
            _ramDistance = ramDistance;
            _ramMultiplier = ramMultiplier;
        }

        public Vector2 CalculateForce()
        {
            float dist = Vector2.Distance(_agent.position, _target.position);
            if (dist < _ramDistance)
            {
                Vector2 dir = (_target.position - _agent.position).normalized;
                return dir * _acceleration * _ramMultiplier;
            }
            return Vector2.zero;
        }
    }
}