using System;
using _Root.Code.CarFeature;
using _Root.Code.LevelFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using UnityEngine;

namespace _Root.Code
{
    public class Root : MonoBehaviour
    {
        [Header("Cinemachine")]
        [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;
        [Header("Car references")] 
        [Tooltip("Cars scriptable objects")]
        [SerializeField] private CarSO _carSo;

        [Tooltip("Level settings")] 
        [SerializeField] private LevelView _levelViewPrefab;
        
        
        
        
        private UpdateManager _updateManager;
        

        private void Start()
        {
            _updateManager = new UpdateManager();
            var initializeManager = new InitializeManager(_carSo, _updateManager, _cinemachineTargetGroup, _levelViewPrefab);
            initializeManager.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _updateManager.Update(deltaTime);
        }
    }
}