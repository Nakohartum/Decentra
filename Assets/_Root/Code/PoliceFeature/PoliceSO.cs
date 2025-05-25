using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    [CreateAssetMenu(fileName = nameof(PoliceSO), menuName = "Create/Police/"+nameof(PoliceSO), order = 0)]
    public class PoliceSO : ScriptableObject, IPoliceModel
    {
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set;}
        [field: SerializeField] public float AvoidForce { get; private set; }
        [field: SerializeField] public float AvoidDistance { get; private set; }
        [field: SerializeField] public float RamDistance { get; private set;}
        [field: SerializeField] public float RamMultiplier { get;private set; }
        [field: SerializeField] public LayerMask ObstacleLayers { get; private set; }
        [field: SerializeField] public PoliceView PolicePrefab { get; private set; }
        [field: SerializeField] public AudioClip PoliceSounds { get; private set; }
        
    }
}