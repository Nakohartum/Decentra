namespace _Root.Code.HackFeature
{
    public class HackModel
    {
        public int RequiredHits { get; private set; }
        public int CurrentHits { get; private set; }
        
        public HackModel(int requiredHits)
        {
            RequiredHits = requiredHits;
            CurrentHits = 0;
        }

        public HackStatus IsHitSuccessful(bool success)
        {
            if (!success)
            {
                return HackStatus.Failed;
            }

            CurrentHits++;
            if (RequiredHits == CurrentHits)
            {
                return HackStatus.Completed;
            }
            else
            {
                return HackStatus.InProgress;
            }
        }

        public void Reset()
        {
            CurrentHits = 0;
        }
    }

    public enum HackStatus
    {
        InProgress,
        Completed,
        Failed
    }
}
