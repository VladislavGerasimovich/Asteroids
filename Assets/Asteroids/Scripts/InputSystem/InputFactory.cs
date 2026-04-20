namespace Asteroids.Scripts.PlayerShip
{
    public class InputFactory
    {
        public IInputService GetGamepadInputService()
        {
            return new GamepadInputService();
        }

        public IInputService GetStandaloneInputService()
        {
            return new StandaloneInputService();
        }

        public IInputService GetMobileJoystickInputService(MobileInputView mobileInputView)
        {
            return new MobileJoystickInputService(mobileInputView);
        }
    }
}