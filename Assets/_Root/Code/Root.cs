using System;
using _Root.Code.CarFeature;
using _Root.Code.LevelFeature;
using _Root.Code.PoliceFeature;
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

        [Header("Level settings")] 
        [SerializeField] private LevelView _levelViewPrefab;
        
        [Header("Police settings")]
        [SerializeField] private PoliceView _policeViewPrefab;

        [SerializeField] private PoliceSO _policeSo;
        
        
        private UpdateManager _updateManager;
        

        private void Start()
        {
            _updateManager = new UpdateManager();
            var initializeManager = new InitializeManager(_carSo, _updateManager, _cinemachineTargetGroup, _levelViewPrefab, _policeSo);
            initializeManager.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _updateManager.Update(deltaTime);
        }
    }
}