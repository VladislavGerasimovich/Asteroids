using MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.Views
{
    public class PlayerRotationView : MonoBehaviour
    {
        [Data("ShipRotation")]
        public TMP_Text rotation;
    }
}