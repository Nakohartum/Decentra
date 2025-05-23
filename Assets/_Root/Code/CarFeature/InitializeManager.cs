using _Root.Code.InputFeature;
using _Root.Code.UpdateFeature;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class InitializeManager
    {
        private readonly CarSO _carSo;
        private readonly UpdateManager _updateManager;
        private readonly Transform _carSpawnPosition;

        public InitializeManager(CarSO carSo, UpdateManager updateManager, Transform carSpawnPosition)
        {
            _carSo = carSo;
            _updateManager = updateManager;
            _carSpawnPosition = carSpawnPosition;
        }

        public void Initialize()
        {
            var inputController = new InputController();
            _updateManager.AddUpdatable(inputController);
            var carFactory = new CarFactory(inputController);
            var carPresenter = carFactory.CreateCar(_carSo, _carSpawnPosition.position, _carSpawnPosition.rotation);
        }
    }
}