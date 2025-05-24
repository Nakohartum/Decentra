using System;
using _Root.Code.CarFeature;
using _Root.Code.GameLoseWinFeature;
using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.Player;
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
        
        [Header("Player settings")]
        [SerializeField] private PlayerSO _playerSo;
        
        [Header("Police settings")]
        [SerializeField] private PoliceView _policeViewPrefab;

        [SerializeField] private PoliceSO _policeSo;
        
        [Header("Game settings")]
        [SerializeField] private InputView _inputView;

        [SerializeField] private LoseWinView _loseWinView;
        
        
        private UpdateManager _updateManager;

        private void Awake()
        {
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            _updateManager = new UpdateManager();
            var initializeManager = new InitializeManager(_carSo, _updateManager, _cinemachineTargetGroup, 
                _levelViewPrefab, _policeSo, _playerSo, _inputView, _loseWinView);
            initializeManager.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _updateManager.Update(deltaTime);
        }

        private void FixedUpdate()
        {
            _updateManager.FixedUpdate();
        }
    }
}