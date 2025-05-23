using UnityEngine;

namespace _Root.Code.LevelFeature
{
    public class LevelView : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnPosition { get; private set; }
    }
}