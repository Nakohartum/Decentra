using System.ComponentModel.Design;

namespace _Root.Code.HackFeature.HackModel
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

        public bool IsHitSuccessful(bool success)
        {
            if (!success) return false;

            CurrentHits++;
            return CurrentHits >= RequiredHits;
        }

        public void Reset()
        {
            CurrentHits = 0;
        }
    }
}
