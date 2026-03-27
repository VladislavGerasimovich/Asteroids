using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipInputRouter : ITickable
    {
        private ShipMovement _shipMovement;
        private PlayerShipView _currentShipView;
        private InertMovement _inertMovement;
        private IInputService _inputService;

        public ShipInputRouter(PlayerShipView shipView, ShipMovement shipMovement, MobileJoystick mobileJoystick)
        {
            _currentShipView = shipView;
            _shipMovement = shipMovement;
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
                
                mobileJoystick.Hide();
            }
            else
            {
                _inputService = new MobileJoystickInputService(mobileJoystick);
                mobileJoystick.Show();
            }
        }
        
        public void Tick()
        {
            if (_inputService.TempAxis.y > 0)
            {
                _inertMovement.Accelerate(_shipMovement.Forward);
            }
            else
            {
                _inertMovement.Slowdown();
            }

            if (_inputService.TempAxis.x != 0)
            {
                _shipMovement.Rotate(_inputService.TempAxis.x);
            }
            
            _shipMovement.MoveLooped(_inertMovement.Acceleration);
            _currentShipView.Move(_shipMovement.Position, _shipMovement.Rotation);
        }
    }
}