using MVVM;
using TMPro;
using UnityEngine;

namespace Asteroids.Scripts.Views
{
    public class LaserGunRollbackView : MonoBehaviour
    {
        [Data("LaserGunRollbackInfo")]
        public TMP_Text rollbackInfo;
    }
}