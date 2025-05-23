using UnityEngine;

namespace _Root.Code.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerSO), menuName = "Create/Player/"+nameof(PlayerSO), order = 0)]
    public class PlayerSO : ScriptableObject, IPlayerModel
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public PlayerView PlayerViewPrefab { get; private set; }
    }
}