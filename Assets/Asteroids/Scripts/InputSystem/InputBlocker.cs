using System;
using Asteroids.Scripts.OwnPhysics;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class InputBlocker : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        
        public bool IsInputEnabled { get; private set; }

        private InputBlocker(CollisionsRecords collisionsRecords)
        {
            _collisionsRecords = collisionsRecords;
        }
        
        public void Initialize()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy += BlockInput;
            IsInputEnabled = true;
        }
        
        public void Dispose()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= BlockInput;
        }
        
        private async void BlockInput()
        {
            IsInputEnabled = false;
            await UniTask.WaitForSeconds(3);
            IsInputEnabled = true;
        }
    }
}