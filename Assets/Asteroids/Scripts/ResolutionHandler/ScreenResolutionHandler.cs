using Asteroids.Scripts.SaveSystem;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ResolutionHandler
{
    public class ScreenResolutionHandler : IInitializable
    {
        private DataManager _dataManager;

        public ScreenResolutionHandler(DataManager dataManager)
        {
            _dataManager = dataManager;
            _dataManager.LoadProgressOrInitNew();
        }
        
        public void Initialize()
        {
            Screen.SetResolution(_dataManager.ScreenWidth, _dataManager.ScreenHeight, _dataManager.IsFullScreen);
        }
    }
}