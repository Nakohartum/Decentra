using UnityEngine;

namespace _Root.Code.PoliceFeature.Strategies
{
    public class AvoidanceSteeringStrategy : ISteeringStrategy
    {
        private readonly Rigidbody2D _agentRb;
        private readonly LayerMask _obstacleMask;
        private readonly float _avoidanceDistance;
        private readonly float _avoidanceForce;
        private readonly float[] _angles = { 0f, 30f, -30f, 60f, -60f };

        public AvoidanceSteeringStrategy(Rigidbody2D agentRb, LayerMask obstacleMask, float avoidanceDistance, float avoidanceForce)
        {
            _agentRb = agentRb;
            _obstacleMask = obstacleMask;
            _avoidanceDistance = avoidanceDistance;
            _avoidanceForce = avoidanceForce;
        }


        public Vector2 CalculateForce()
        {
            Vector2 forward = _agentRb.velocity.normalized;
            Vector2 totalAvoidance = Vector2.zero;

            foreach (float angle in _angles)
            {
                Vector2 dir = Quaternion.Euler(0, 0, angle) * forward;
                RaycastHit2D hit = Physics2D.Raycast(_agentRb.position, dir, _avoidanceDistance, _obstacleMask);
                if (hit.collider != null)
                {
                    Vector2 reflectDir = Vector2.Reflect(forward, hit.normal).normalized;
                    float weight = 1f - (hit.distance / _avoidanceDistance);
                    totalAvoidance += reflectDir * weight;
                }
            }
            return totalAvoidance.normalized * _avoidanceForce;
        }
    }
}