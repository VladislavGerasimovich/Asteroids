using MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.Views
{
    public class PlayerPositionView : MonoBehaviour
    {
        [Data("ShipPosition")]
        public TMP_Text position;
    }
}