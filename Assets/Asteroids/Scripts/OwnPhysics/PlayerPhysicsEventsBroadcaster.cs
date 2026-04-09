using Cysharp.Threading.Tasks;

namespace Asteroids.Scripts.OwnPhysics
{
    public class PlayerPhysicsEventsBroadcaster : PhysicsEventsBroadcaster
    {
        private CollisionsRecords _collisionsRecords;
        
        public void Init(PhysicsRouter router, object model, CollisionsRecords collisionsRecords)
        {
            Router = router;
            Model = model;
            _collisionsRecords = collisionsRecords;
            _collisionsRecords.OnPlayerCollideWithEnemy += DisableCollisions;
            CanHandleCollisions = true;
        }

        private void OnDisable()
        {
            if (_collisionsRecords != null)
            {
                _collisionsRecords.OnPlayerCollideWithEnemy -= DisableCollisions;
            }
        }

        private async void DisableCollisions()
        {
            CanHandleCollisions = false;
            await UniTask.WaitForSeconds(3f);
            CanHandleCollisions = true;
        }
    }
}