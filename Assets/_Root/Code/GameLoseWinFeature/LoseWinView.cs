using UnityEngine;
using UnityEngine.UI;

namespace _Root.Code.GameLoseWinFeature
{
    public class LoseWinView : MonoBehaviour
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [field: SerializeField] public Button TryAgainButton { get; private set; }

        public void ShowWinScreen()
        {
            _winPanel.SetActive(true);
            TryAgainButton.gameObject.SetActive(true);
        }

        public void HideWinScreen()
        {
            _winPanel.SetActive(false);
            TryAgainButton.gameObject.SetActive(false);
        }

        public void ShowLoseScreen()
        {
            _losePanel.SetActive(true);
            TryAgainButton.gameObject.SetActive(true);
        }

        public void HideLoseScreen()
        {
            _losePanel.SetActive(false);
            TryAgainButton.gameObject.SetActive(false);
        }
    }
}