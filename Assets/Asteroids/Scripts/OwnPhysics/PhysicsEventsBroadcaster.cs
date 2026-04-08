using UnityEngine;

namespace Asteroids.Scripts.OwnPhysics
{
    public class PhysicsEventsBroadcaster : MonoBehaviour
    {
        public PhysicsRouter Router { get; protected set; }
        public object Model { get; protected set; }
        public bool CanHandleCollisions { get; protected set; }

        public void Init(PhysicsRouter router, object model)
        {
            Router = router;
            Model = model;
            CanHandleCollisions = true;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out PhysicsEventsBroadcaster broadcaster))
            {
                if (broadcaster.CanHandleCollisions && CanHandleCollisions)
                {
                    Router.TryAddCollision(Model, broadcaster.Model);
                }
            }
        }
    }
}