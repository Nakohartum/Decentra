using UnityEngine;

namespace _Root.Code.LevelFeature
{
    public class Spawnpoint : MonoBehaviour
    {
        [field: SerializeField] public Transform[] CarsSpawnPoints { get; private set; }
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Transform[] PoliceSpawnPoints { get; private set; }
    }
}