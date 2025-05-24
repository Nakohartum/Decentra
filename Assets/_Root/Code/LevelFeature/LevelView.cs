using _Root.Code.GameLoseWinFeature;
using UnityEngine;

namespace _Root.Code.LevelFeature
{
    public class LevelView : MonoBehaviour
    {
        [field: SerializeField] public WinTrigger[] WinTrigger { get; private set; }
        [field: SerializeField] public Spawnpoint Spawnpoint { get; private set; }
        
        
        private WinTrigger _currentWinTrigger;

        public WinTrigger GetWinTrigger()
        {
            _currentWinTrigger ??= GetRandomWinTrigger();
            return _currentWinTrigger;
        }

        private WinTrigger GetRandomWinTrigger()
        {
            return WinTrigger[Random.Range(0, WinTrigger.Length)];
        }
    }
}