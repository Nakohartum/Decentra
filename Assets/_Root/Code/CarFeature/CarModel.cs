namespace _Root.Code.CarFeature
{
    public class CarModel : ICarModel
    {
        public float Speed { get; private set; }
        public float Health { get; private set; }
        public float Acceleration { get; private set;}
        public float TurnSpeed { get; private set; }

        public CarModel(float speed, float health, float acceleration, float turnSpeed)
        {
            Speed = speed;
            Health = health;
            Acceleration = acceleration;
            TurnSpeed = turnSpeed;
        }
    }
}