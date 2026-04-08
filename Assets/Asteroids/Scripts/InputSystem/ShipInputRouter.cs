using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipInputRouter : ITickable
    {
        private ShipMovement _shipMovement;
        private PlayerSpawner _playerSpawner;
        private InertMovement _inertMovement;
        private IInputService _inputService;
        private ShipWeaponsHandler _shipWeaponsHandler;
        private InputBlocker _inputBlocker;

        public ShipInputRouter(
            PlayerSpawner playerSpawner,
            ShipMovement shipMovement,
            ShipWeaponsHandler shipWeaponsHandler,
            MobileInputView mobileInputView,
            InputBlocker inputBlocker)
        {
            _playerSpawner = playerSpawner;
            _shipMovement = shipMovement;
            _shipWeaponsHandler = shipWeaponsHandler;
            _inputBlocker = inputBlocker;
            _inertMovement = new InertMovement();
            
            if (Application.isEditor)
            {
                bool hasGamepad = false;
                string[] joysticks = Input.GetJoystickNames();
                
                for (int i = 0; i < joysticks.Length; i++)
                {
                    if (!string.IsNullOrEmpty(joysticks[i]))
                    {
                        hasGamepad = true;
                        _inputService = new GamepadInputService();
                    }
                }

                if(hasGamepad == false)
                {
                    _inputService = new StandaloneInputService();
                }
                
                mobileInputView.Hide();
            }
            else
            {
                _inputService = new MobileJoystickInputService(mobileInputView);
                mobileInputView.Show();
            }
        }

        public void Tick()
        {
            if (_inputBlocker.IsInputEnabled == true)
            {
                if (_inputService.TempAxis.y > 0)
                {
                    _inertMovement.Accelerate(_shipMovement.Forward);
                }
                else
                {
                    _inertMovement.Slowdown();
                }
            }
            else
            {
                _inertMovement.Slowdown();
            }

            if (_inputService.TempAxis.x != 0)
            {
                if (_inputBlocker.IsInputEnabled == true)
                    _shipMovement.Rotate(_inputService.TempAxis.x);
            }

            if (_inputBlocker.IsInputEnabled == true)
            {
                if (_inputService.IsFirstGunSlotButtonDown())
                {
                    _shipWeaponsHandler.OnFirstSlotGunButtonClicked();
                }
                else if (_inputService.IsSecondGunSlotButtonDown())
                {
                    _shipWeaponsHandler.OnSecondSlotGunButtonClicked();
                }
            }

            _shipMovement.MoveLooped(_inertMovement.Acceleration);
            _playerSpawner.CurrentPlayerShipView.Move(_shipMovement.Position, _shipMovement.Rotation);
        }
    }
}