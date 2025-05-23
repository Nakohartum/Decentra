using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Code.HackFeature.HackGameController
{
    public class HackGameController : MonoBehaviour
    {
        public RectTransform pointer;
        public List<RectTransform> successZones;
        public HackView.HackView HackView;
        public Button hackButton;

        public int requiredHits = 3;
        public float zoneSize = 30f;
        
        private HackPresenter.HackPresenter _presenter;
        
        private void Awake()
        {
            _presenter = new HackPresenter.HackPresenter(HackView, new HackModel.HackModel(requiredHits));
            CreateRandomSuccessZones();
            hackButton.onClick.AddListener(OnHackButtonPressed);
        }

        private void Update()
        {
            pointer.Rotate(0, 0, -180f * Time.deltaTime);
        }

        private void OnHackButtonPressed()
        {
            var pointerAngle = NormalizeAngle(pointer.eulerAngles.z);
            var anyZone = false;

            foreach (var zone in successZones)
            {
                var zoneAngle = NormalizeAngle(zone.eulerAngles.z);
                if (Mathf.Abs(Mathf.DeltaAngle(pointerAngle, zoneAngle)) < zoneSize / 2f)
                {
                    anyZone = true;
                    break;
                }
            }
            
            _presenter.OnPlayPressed(anyZone);
        }

        private void CreateRandomSuccessZones()
        {
            foreach (var zone in successZones)
            {
                var zoneAngleZ = Random.Range(0f, 360f);
                zone.localEulerAngles = new Vector3(0, 0, zoneAngleZ);
            }
        }

        private float NormalizeAngle(float angle)
        {
            angle = angle % 360f;
            return angle < 0 ? angle + 360f : angle;
        } 
    }
}