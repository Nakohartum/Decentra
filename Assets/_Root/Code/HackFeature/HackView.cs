using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Root.Code.HackFeature
{
    public class HackView : MonoBehaviour, IHackView
    {
        public TextMeshProUGUI statusText;
        public RectTransform pointer;
        public List<RectTransform> successZones;
        [FormerlySerializedAs("hackButton")] public Button HackButton;
        public RectTransform circle;
        public float zoneSize;
        
        public void ShowSuccess(int current, int required)
        {
            statusText.text = $"Успех! {current}/{required}";
        }

        public void ShowFailure()
        {
            statusText.text = "Неудача.";
        }

        public void ShowHackComplete()
        {
            statusText.text = "Успешно взломано!";
        }
    }
}