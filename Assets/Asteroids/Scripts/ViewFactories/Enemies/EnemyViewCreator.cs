using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.Utils;
using UnityEngine;

namespace Asteroids.Scripts.ViewFactories.Enemies
{
    public class EnemyViewCreator
    {
        private EnemiesViewFactory _enemiesViewFactory;
        private Camera _camera;
        private PhysicsRouter _physicsRouter;
        private PositionUtils _positionUtils;

        public EnemyViewCreator(
            PositionUtils positionUtils,
            PhysicsRouter physicsRouter,
            Camera camera,
            EnemiesViewFactory enemiesViewFactory)
        {
            _positionUtils = positionUtils;
            _physicsRouter = physicsRouter;
            _camera = camera;
            _enemiesViewFactory = enemiesViewFactory;
        }
        
        public EnemyView CreateView(Enemy enemy)
        {
            EnemyView enemyView = _enemiesViewFactory.GetTemplate(enemy);
            enemyView.Init(_camera, enemy);

            PhysicsEventsBroadcaster physicsEventsBroadcaster = enemyView.GetComponent<PhysicsEventsBroadcaster>();
            physicsEventsBroadcaster.Init(_physicsRouter, enemy);

            if (enemyView.TryGetComponent(out BoxCollider2D boxCollider))
                boxCollider.enabled = true;

            enemyView.transform.position =
                _camera.ViewportToWorldPoint(_positionUtils.GetRandomPositionInsideUnitCircle());
            enemyView.gameObject.SetActive(true);

            return enemyView;
        }
    }
}