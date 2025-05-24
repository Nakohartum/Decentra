namespace _Root.Code.CarFeature
{
    public class CarModel : ICarModel
    {
        public float Speed { get; private set; }
        public float Health { get; private set; }
        public float Acceleration { get; private set;}
        public float TurnSpeed { get; private set; }
        public float SideFriction { get; private set;}
        public float MaxHealth { get; private set;}

        public CarModel(float speed, float health, float acceleration, float turnSpeed, float sideFriction)
        {
            Speed = speed;
            Health = health;
            MaxHealth = health;
            Acceleration = acceleration;
            TurnSpeed = turnSpeed;
            SideFriction = sideFriction;
        }

        public void ApplyDamage(float damage)
        {
            Health -= damage;
        }
    }
}