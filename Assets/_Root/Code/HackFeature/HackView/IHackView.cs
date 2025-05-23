namespace _Root.Code.HackFeature.HackModel
{
    public interface IHackView
    {
        public void ShowSuccess(int current, int required);
        
        public void ShowFailure();
        
        public void ShowHackComplete();
    }
}