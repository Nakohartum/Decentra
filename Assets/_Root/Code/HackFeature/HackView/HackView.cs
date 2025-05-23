using _Root.Code.HackFeature.HackModel;
using TMPro;

namespace _Root.Code.HackFeature.HackView
{
    public class HackView : IHackView
    {
        public TextMeshProUGUI StatusText;
        
        public void ShowSuccess(int current, int required)
        {
            StatusText.text = $"Успех! {current}/{required}";
        }

        public void ShowFailure()
        {
            StatusText.text = "Неудача.";
        }

        public void ShowHackComplete()
        {
            StatusText.text = "Успешно взломано!";
        }
    }
}