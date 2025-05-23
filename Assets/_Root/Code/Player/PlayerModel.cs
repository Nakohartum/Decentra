using _Root.Code.Player;

namespace Player
{
    public class PlayerModel : IPlayerModel
    {
        public float Speed { get; private set; }

        public PlayerModel(float moveSpeed)
        {
            Speed = moveSpeed;
        }
    } 
    
}