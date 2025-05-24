using _Root.Code.UpdateFeature;
using UnityEngine;

namespace _Root.Code.NavigationFeature
{
    public class NavigationPresenter : IUpdatable
    {
        private NavigationView _navigationView;
        private Transform _target;
        private Transform _player;
        private bool _isNavigating = false;

        public NavigationPresenter(NavigationView navigationView, Transform target)
        {
            _navigationView = navigationView;
            _target = target;
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
        }

        public void Dispose()
        {
            DisableArrow();
        }

        public void EnableArrow()
        {
            _navigationView.gameObject.SetActive(true);
            _isNavigating = true;
        }
        
        public void DisableArrow()
        {
            _navigationView.gameObject.SetActive(false);
            _isNavigating = false;
        }

        public void Update(float deltaTime)
        {
            if (!_isNavigating)
                return;
            CalculateAngle();
        }

        private void CalculateAngle()
        {
            Vector3 dir = _target.position - _player.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _navigationView.transform.rotation = Quaternion.Euler(0f,0f, angle - 90f);
        }
    }
}