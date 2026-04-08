using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Scripts.OwnPhysics
{
    public class PlayerPhysicsEventsBroadcaster : PhysicsEventsBroadcaster
    {
        private CollisionsRecords _collisionsRecords;
        private bool _canHandleCollisions;
        
        public void Init(PhysicsRouter router, object model, CollisionsRecords collisionsRecords)
        {
            Router = router;
            Model = model;
            _collisionsRecords = collisionsRecords;
            _collisionsRecords.OnPlayerCollideWithEnemy += DisableCollisions;
        }

        private void OnDisable()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= DisableCollisions;
        }

        private async void DisableCollisions()
        {
            _canHandleCollisions = false;
            await UniTask.WaitForSeconds(3f);
            _canHandleCollisions = true;
        }
        
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if(_canHandleCollisions)
                base.OnCollisionEnter2D(collision);
        }
    }
}