using System.Threading;
using Asteroids.Scripts.SaveSystem;
using Cysharp.Threading.Tasks;

namespace Asteroids.Scripts.OwnPhysics
{
    public class PlayerPhysicsEventsBroadcaster : PhysicsEventsBroadcaster
    {
        private CollisionsRecords _collisionsRecords;
        private SaveDataRepository _saveDataRepository;
        private CancellationTokenSource _cts;
        
        public void Init(
            PhysicsRouter router,
            object model,
            CollisionsRecords collisionsRecords,
            SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            Router = router;
            Model = model;
            _collisionsRecords = collisionsRecords;
            _collisionsRecords.OnPlayerCollideWithEnemy += OnPlayerEnemyCollision;
            CanHandleCollisions = true;
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (_collisionsRecords != null)
            {
                _collisionsRecords.OnPlayerCollideWithEnemy -= OnPlayerEnemyCollision;
            }
        }

        private void OnPlayerEnemyCollision()
        {
            DisableCollisions();
        }
        
        private async UniTask DisableCollisions()
        {
            CanHandleCollisions = false;
            await UniTask.WaitForSeconds(_saveDataRepository.BouncingTime, false, PlayerLoopTiming.FixedUpdate, _cts.Token);
            CanHandleCollisions = true;
        }
    }
}