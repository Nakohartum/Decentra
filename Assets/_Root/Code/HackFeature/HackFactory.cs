using UnityEngine;

namespace _Root.Code.HackFeature
{
    public class HackFactory
    {
        private HackView _viewPrefab;

        public HackFactory(HackView viewPrefab)
        {
            _viewPrefab = viewPrefab;
        }

        public HackPresenter CreateHackPresenter()
        {
            var presenter = new HackPresenter(_viewPrefab, new HackModel(3), this);
            _viewPrefab.hackButton.onClick.AddListener(presenter.OnHackButtonPressed);
            CreateRandomSuccessZone(_viewPrefab);
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
        
        public void DrawSuccessZone(Transform parent, float angle, float width = 40f, float radius = 1f, int segments = 20)
        {
            var go = new GameObject("SuccessZone");
            go.transform.SetParent(parent, false);
            var line = go.AddComponent<LineRenderer>();

            line.positionCount = segments + 1;
            line.useWorldSpace = false;
            line.widthMultiplier = 0.1f;
            line.loop = false;

            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.green;
            line.endColor = Color.green;

            float startAngle = angle - width / 2f;
            for (int i = 0; i <= segments; i++)
            {
                float currentAngle = startAngle + i * (width / segments);
                float rad = currentAngle * Mathf.Deg2Rad;
                Vector3 point = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
                line.SetPosition(i, point);
            }
        }

    }
}