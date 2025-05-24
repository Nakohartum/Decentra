namespace _Root.Code.HackFeature
{
    public interface IHackView
    {
        public void ShowSuccess(int current, int required);
        
        public void ShowFailure();
        
        public void ShowHackComplete();
    }
}