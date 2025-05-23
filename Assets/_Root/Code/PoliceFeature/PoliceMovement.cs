using System.Collections.Generic;
using _Root.Code.MoveFeature;
using _Root.Code.PoliceFeature.Strategies;
using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceMovement : IMovable
    {
        private readonly PoliceView _policeView;
        private readonly PoliceModel _policeModel;
        private readonly List<ISteeringStrategy> _steeringStrategies;

        public PoliceMovement(List<ISteeringStrategy> steeringStrategies, PoliceView policeView, PoliceModel policeModel)
        {
            _steeringStrategies = steeringStrategies;
            _policeView = policeView;
            _policeModel = policeModel;
        }

        public void Move(Vector3 direction)
        {
            var forceVector = CalculateForce();
            ApplyForce(forceVector);
        }
        
        private void ApplyForce(Vector2 totalForce)
        {
            _policeView.Rigidbody2D.AddForce(totalForce);
            if (_policeView.Rigidbody2D.velocity.magnitude > _policeModel.MaxSpeed)
            {
                _policeView.Rigidbody2D.velocity = _policeView.Rigidbody2D.velocity.normalized * _policeModel.MaxSpeed;
            }
        }

        private Vector2 CalculateForce()
        {
            bool hasObstacle = Physics2D.Raycast(
                _policeView.Rigidbody2D.position,
                _policeView.Rigidbody2D.velocity.normalized,
                _policeModel.AvoidDistance,
                _policeModel.ObstacleLayers
            );
            Vector2 totalForce = Vector2.zero;
            foreach (ISteeringStrategy strategy in _steeringStrategies)
            {
                if (hasObstacle && strategy is PursueSteeringStrategy)
                {
                    continue;
                }
                totalForce += strategy.CalculateForce();
            }

            return totalForce;
        }

        public void Rotate(Vector3 direction)
        {
            var rb = _policeView.Rigidbody2D;
            if (rb.velocity.sqrMagnitude > 0.1f)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
                rb.MoveRotation(angle);
            }
        }
    }
}