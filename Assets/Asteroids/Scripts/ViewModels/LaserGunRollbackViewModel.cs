using System;
using Asteroids.Scripts.Guns;
using MVVM;
using UniRx;
using Zenject;

namespace Asteroids.Scripts.ViewModels
{
    public class LaserGunRollbackViewModel : ITickable
    {
        [Data("LaserGunRollbackInfo")]
        public ReactiveProperty<string> LaserGunRollbackInfo;
        
        private LaserGunRollback _laserGunRollback;

        public LaserGunRollbackViewModel()
        {
            LaserGunRollbackInfo = new ReactiveProperty<string>($"Rollback: 0,0");
        }

        public void Init(LaserGunRollback laserGunRollback)
        {
            _laserGunRollback = laserGunRollback;
        }

        public void Tick()
        {
            if (_laserGunRollback == null)
                return;
            
            if (LaserGunRollbackInfo.Value != _laserGunRollback.AccumulatedTime.ToString())
            {
                LaserGunRollbackInfo.Value = $"Rollback: {Math.Round(_laserGunRollback.AccumulatedTime, 2)}";
            }
        }
    }
}