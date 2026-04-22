using UnityEngine;

namespace Asteroids.Scripts.InputSystem
{
    public interface IInputService
    {
        Vector2 TempAxis { get; }
        bool IsFirstGunSlotButtonDown();
        bool IsSecondGunSlotButtonDown();
    }
}