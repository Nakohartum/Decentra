using _Root.Code.InputFeature;
using UnityEngine;

namespace _Root.Code.HackFeature
{
    public class HackFactory
    {
        private InputView _viewPrefab;

        public HackFactory(InputView viewPrefab)
        {
            _viewPrefab = viewPrefab;
        }

        public HackPresenter CreateHackPresenter()
        {
            var presenter = new HackPresenter(_viewPrefab.HackView, new HackModel(3), this);
            _viewPrefab.HackView.gameObject.SetActive(true);
            _viewPrefab.HackView.HackButton.onClick.AddListener(presenter.OnHackButtonPressed);
            CreateRandomSuccessZone(_viewPrefab.HackView);
            return presenter;
        }
        
        public void CreateRandomSuccessZone(HackView view)
        {
            foreach (var zone in view.successZones)
            {
                var zoneAngleZ = Random.Range(0f, 360f);
                zone.localEulerAngles = new Vector3(0, 0, zoneAngleZ);
            }
            
        }
    }
}