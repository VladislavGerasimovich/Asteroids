using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Scripts.Views
{
    public class EndGamePopupWindowView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [Data("RestartButton")]
        public Button restartButton;

        [Data("RewardsCount")]
        public TMP_Text rewardsCount;

        private void Awake()
        {
            Hide();
        }

        [Method("IsPlayerDied")]
        public void Show(bool isPlayerDied)
        {
            if (isPlayerDied == true)
            {
                rewardsCount.text =
                    $"Your reward: {(string.IsNullOrEmpty(rewardsCount.text) ? "0" : rewardsCount.text)}";
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
        
        private void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}