using UnityEngine;

namespace _Root.Code.CarFeature
{
    [CreateAssetMenu(fileName = nameof(CarSO), menuName = "Create/Car/"+nameof(CarSO), order = 0)]
    public class CarSO : ScriptableObject, ICarModel
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Health { get; private set;}
        [field: SerializeField] public float Acceleration { get; private set;}
        [field: SerializeField] public float TurnSpeed { get; private set;}
        [field: SerializeField] public float Mass { get; private set;}
        [field: SerializeField] public float LinearDrag { get; private set;}
        [field: SerializeField] public float AngularDrag { get; private set;}
        [field: SerializeField] public float SideFriction { get; private set;}
        [field: SerializeField] public CarView CarPrefab { get; private set; }
        [field: SerializeField] public AudioClip CarEngineSound { get; private set;}
        [field: SerializeField] public AudioClip CarOpenSound { get; private set;}
    }
}