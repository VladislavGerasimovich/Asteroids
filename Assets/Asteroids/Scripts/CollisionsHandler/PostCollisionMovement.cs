using System;
using Asteroids.Scripts.OwnPhysics;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class PostCollisionMovement : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;

        public PostCollisionMovement(CollisionsRecords collisionsRecords)
        {
            _collisionsRecords = collisionsRecords;
        }
        
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}