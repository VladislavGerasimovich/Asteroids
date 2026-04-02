using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public interface IInputService
    {
        Vector2 TempAxis { get; }
        bool IsFirstGunSlotButtonDown();
        bool IsSecondGunSlotButtonDown();
    }
}