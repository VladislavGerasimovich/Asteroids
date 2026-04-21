using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.SaveSystem
{
#if UNITY_EDITOR
    public class DebugSaveResetService : ITickable
    {
        private GameBalanceConfig _gameBalanceConfig;
        private SaveBootstrapper _saveBootstrapper;

        public DebugSaveResetService(SaveBootstrapper saveBootstrapper, GameBalanceConfig gameBalanceConfig)
        {
            _saveBootstrapper = saveBootstrapper;
            _gameBalanceConfig = gameBalanceConfig;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _gameBalanceConfig.InitNewData(_saveBootstrapper.Data);
                _saveBootstrapper.Save();
            }
        }
    }
#endif
}