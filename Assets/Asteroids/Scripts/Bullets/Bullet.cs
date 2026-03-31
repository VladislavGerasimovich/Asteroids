namespace Asteroids.Scripts.Bullets
{
    public abstract class Bullet
    {
        public readonly float Lifetime;
        public readonly float Speed;

        protected Bullet(float lifetime, float speed)
        {
            Lifetime = lifetime;
            Speed = speed;
        }
    }
}