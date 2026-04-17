namespace Asteroids.Scripts.Bullets
{
    public class BulletEntity
    {
        public readonly Bullet Entity;
        public readonly Trajectory BulletTrajectory;

        public BulletEntity(Bullet entity, Trajectory trajectory)
        {
            Entity = entity;
            BulletTrajectory = trajectory;
        }
    }
}