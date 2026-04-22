using Asteroids.Scripts.SaveSystem;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ResolutionHandler
{
    public class ScreenResolutionHandler : IInitializable
    {
        private SaveDataRepository _saveDataRepository;

        public ScreenResolutionHandler(SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
        }
        
        public void Initialize()
        {
            Screen.SetResolution(_saveDataRepository.ScreenWidth, _saveDataRepository.ScreenHeight, _saveDataRepository.IsFullScreen);
        }
    }
}