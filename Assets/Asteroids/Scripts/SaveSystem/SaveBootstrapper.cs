using Zenject;

namespace Asteroids.Scripts.SaveSystem
{
    public class SaveBootstrapper
    {
        private SaveData _saveData;
        private JsonSaver _jsonSaver;
        private GameBalanceConfig _gameBalanceConfig;
        private bool _hasSavedData;
        private ITickable _tickableImplementation;

        public SaveData Data => _saveData;
        
        public SaveBootstrapper(GameBalanceConfig gameBalanceConfig)
        {
            _gameBalanceConfig = gameBalanceConfig;
            _saveData = new SaveData();
            _jsonSaver = new JsonSaver();
            LoadProgressOrInitNew();
        }
        
        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }
        
        private void LoadProgressOrInitNew()
        {
            _hasSavedData = _jsonSaver.Load(ref _saveData);
            
            if(_hasSavedData == false)
            {
                _gameBalanceConfig.InitNewData(_saveData);
                Save();
            }
        }
    }
}