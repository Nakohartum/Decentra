using System;
using _Root.Code.CarFeature;
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
        [Tooltip("Cars spawn positions")]
        [SerializeField] private Transform _carSpawnPoint;
        
        
        
        private UpdateManager _updateManager;
        private InitializeManager _initializeManager;
        

        private void Start()
        {
            _updateManager = new UpdateManager();
            _initializeManager = new InitializeManager(_carSo, _updateManager, _carSpawnPoint, _cinemachineTargetGroup);
            _initializeManager.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _updateManager.Update(deltaTime);
        }
    }
}