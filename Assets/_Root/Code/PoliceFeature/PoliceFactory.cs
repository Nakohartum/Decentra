using System.Collections.Generic;
using _Root.Code.PoliceFeature.Strategies;
using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceFactory
    {
        private PoliceView _policePrefab;
        private PoliceSO _policeSO;
        private Rigidbody2D _target;

        public PoliceFactory(PoliceView policePrefab, PoliceSO policeSo, Rigidbody2D target)
        {
            _policePrefab = policePrefab;
            _policeSO = policeSo;
            _target = target;
        }

        public PolicePresenter CreatePoliceUnit()
        {
            var view = Object.Instantiate(_policePrefab);
            var model = new PoliceModel(_policeSO.MaxSpeed, _policeSO.Acceleration, _policeSO.AvoidForce,
                _policeSO.AvoidDistance, _policeSO.RamDistance, _policeSO.RamMultiplier, _policeSO.ObstacleLayers);
            var strategies = CreatePoliceMovementStrategies(view, model);
            var movable = new PoliceMovement(strategies, view, model);
            var presenter = new PolicePresenter(view, model, movable);
            return presenter;
        }

        private List<ISteeringStrategy> CreatePoliceMovementStrategies(PoliceView agent, PoliceModel model)
        {
            var steeringStrategies = new List<ISteeringStrategy>();
            steeringStrategies.Add(new PursueSteeringStrategy(agent.transform, _target.transform, _target, model.Acceleration));
            
            steeringStrategies.Add(new AvoidanceSteeringStrategy(agent.Rigidbody2D, _policeSO.ObstacleLayers, 
                model.AvoidDistance, model.AvoidForce));
            steeringStrategies.Add(new RamSteeringStrategy(agent.transform, _target.transform, model.Acceleration,
                model.RamDistance, model.RamMultiplier));
            return steeringStrategies;
        }
    }
}