using System;
using System.Threading;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.SaveSystem;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Asteroids.Scripts.InputSystem
{
    public class InputBlocker : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        private SaveDataRepository _saveDataRepository;
        private CancellationTokenSource _cts;

        public bool IsInputEnabled { get; private set; }

        private InputBlocker(CollisionsRecords collisionsRecords, SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _collisionsRecords = collisionsRecords;
            _cts = new CancellationTokenSource();
        }
        
        public void Initialize()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy += OnPlayerEnemyCollision;
            IsInputEnabled = true;
        }
        
        public void Dispose()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= OnPlayerEnemyCollision;
        }
        
        private void OnPlayerEnemyCollision()
        {
            BlockInput();
        }
        
        private async UniTask BlockInput()
        {
            IsInputEnabled = false;
            await UniTask.WaitForSeconds(_saveDataRepository.BouncingTime, false, PlayerLoopTiming.FixedUpdate, _cts.Token);
            IsInputEnabled = true;
        }
    }
}