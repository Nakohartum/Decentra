using System;
using _Root.Code.CarFeature;
using _Root.Code.GameLoseWinFeature;
using _Root.Code.InputFeature;
using _Root.Code.LevelFeature;
using _Root.Code.NavigationFeature;
using _Root.Code.Player;
using _Root.Code.PoliceFeature;
using _Root.Code.StartMenuFeature;
using _Root.Code.UpdateFeature;
using Cinemachine;
using UnityEngine;

namespace _Root.Code
{
    public class Root : MonoBehaviour
    {
        [field: Header("Cinemachine")]
        [field: SerializeField] public CinemachineTargetGroup CinemachineTargetGroup { get; private set; }
        [field: Header("Car references")] 
        [Tooltip("Cars scriptable objects")]
        [field: SerializeField] public CarSO[] CarSo { get; private set; }

        [field: Header("Level settings")] 
        [field: SerializeField] public LevelView LevelViewPrefab { get; private set; }
        
        [field: Header("Player settings")]
        [field: SerializeField] public PlayerSO PlayerSo { get; private set; }
        
        [field: Header("Police settings")]
        [field: SerializeField] public PoliceSO PoliceSo { get; private set; }
        
        [field: Header("Game settings")]
        [field: SerializeField] public InputView InputView { get; private set; }
        [field: SerializeField] public StartMenuView StartMenuView { get; private set; }
        [field: SerializeField] public LoseWinView LoseWinView { get; private set; }
        [field: SerializeField] public NavigationView NavigationView { get; private set; }
        [field: SerializeField] public float MaxPoliceCount { get; private set; }
        [field: SerializeField] public int PoliceSpawnTimer { get; private set; }
        public UpdateManager UpdateManager { get; private set; }
        


        private void Awake()
        {
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            UpdateManager = new UpdateManager();
            var initializeManager = new InitializeManager(this);
            initializeManager.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            UpdateManager.Update(deltaTime);
        }

        private void FixedUpdate()
        {
            UpdateManager.FixedUpdate();
        }
    }
}