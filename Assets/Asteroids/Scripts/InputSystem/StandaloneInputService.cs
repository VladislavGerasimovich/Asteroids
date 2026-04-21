using UnityEngine;

namespace Asteroids.Scripts.InputSystem
{
    public class StandaloneInputService : InputService
    {
        private Vector2 _tempAxis;
        
        public override Vector2 TempAxis
        {
            get
            {
                _tempAxis = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

                return _tempAxis;
            }
        }

        public override bool IsFirstGunSlotButtonDown() => Input.GetMouseButtonDown(0);
        public override bool IsSecondGunSlotButtonDown() => Input.GetMouseButtonDown(1);
    }
}