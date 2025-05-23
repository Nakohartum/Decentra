using UnityEngine;

namespace _Root.Code.LevelFeature
{
    public class LevelFactory
    {
        private LevelView _levelPrefab;

        public LevelFactory(LevelView levelPrefab)
        {
            _levelPrefab = levelPrefab;
        }

        public LevelView CreateLevel()
        {
            return Object.Instantiate(_levelPrefab);
        }
    }
}