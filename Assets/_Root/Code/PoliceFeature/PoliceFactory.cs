using System.Collections.Generic;
using _Root.Code.PoliceFeature.Strategies;
using Unity.Mathematics;
using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceFactory
    {
        private PoliceSO _policeSO;
        private Transform[] _spawnPositions;

        public PoliceFactory(PoliceSO policeSo, Transform[] spawnPositions)
        {
            _policeSO = policeSo;
            _spawnPositions = spawnPositions;
        }

        public float CurrentPoliceCount { get; private set; }

        public PolicePresenter CreatePoliceUnit(Rigidbody2D target)
        {
            CurrentPoliceCount++;
            var randomPoint = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)];
            var view = Object.Instantiate(_policeSO.PolicePrefab, randomPoint.position, quaternion.identity);
            var model = new PoliceModel(_policeSO.MaxSpeed, _policeSO.Acceleration, _policeSO.AvoidForce,
                _policeSO.AvoidDistance, _policeSO.RamDistance, _policeSO.RamMultiplier, _policeSO.ObstacleLayers);
            var strategies = CreatePoliceMovementStrategies(view, model, target);
            var movable = new PoliceMovement(strategies, view, model);
            var presenter = new PolicePresenter(view, model, movable);
            return presenter;
        }

        private List<ISteeringStrategy> CreatePoliceMovementStrategies(PoliceView agent, PoliceModel model, Rigidbody2D target)
        {
            var steeringStrategies = new List<ISteeringStrategy>();
            steeringStrategies.Add(new PursueSteeringStrategy(agent.transform, target.transform, target, model.Acceleration));
            
            steeringStrategies.Add(new AvoidanceSteeringStrategy(agent.Rigidbody2D, _policeSO.ObstacleLayers, 
                model.AvoidDistance, model.AvoidForce));
            steeringStrategies.Add(new RamSteeringStrategy(agent.transform, target.transform, model.Acceleration,
                model.RamDistance, model.RamMultiplier));
            return steeringStrategies;
        }
    }
}