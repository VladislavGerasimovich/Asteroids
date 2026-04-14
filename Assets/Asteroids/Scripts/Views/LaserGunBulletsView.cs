using MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.Views
{
    public class LaserGunBulletsView : MonoBehaviour
    {
        [Data("LaserGunBulletsInfo")]
        public TMP_Text bulletsInfo;
    }
}