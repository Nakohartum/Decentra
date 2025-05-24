using _Root.Code.GameLoseWinFeature;
using UnityEngine;

namespace _Root.Code.LevelFeature
{
    public class LevelView : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnPosition { get; private set; }
        [field: SerializeField] public WinTrigger WinTrigger { get; private set; }
    }
}