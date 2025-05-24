using _Root.Code.UpdateFeature;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Code.StartMenuFeature
{
    public class StartMenuView : MonoBehaviour
    {
        public TMP_Text Title;
        public Button Button;

        public StartMenuView(TMP_Text title, Button button)
        {
            Title = title;
            Button = button;
        }
    }
}