using MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.Views
{
    public class PlayerSpeedView : MonoBehaviour
    {
        [Data("ShipSpeed")]
        public TMP_Text speed;
    }
}