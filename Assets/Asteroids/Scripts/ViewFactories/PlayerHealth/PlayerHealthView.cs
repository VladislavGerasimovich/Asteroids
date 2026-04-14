using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.Scripts.ViewFactories.PlayerHealth
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private HeartView heartView;

        private List<HeartView> _views;
        
        public void CreateViews(int count)
        {
            _views = new List<HeartView>();
            
            for (int i = 0; i < count; i++)
            {
                HeartView view = Instantiate(heartView, container);
                view.Init();
                view.Show();
                _views.Add(view);
            }
        }

        public void HideHeart()
        {
            if (_views == null)
                return;
            
            Debug.Log("[PlayerHealthView] HideHeart");
            var lastActiveHeart = _views.LastOrDefault(h => h.IsActive);
            
            if (lastActiveHeart != null)
            {
                lastActiveHeart.Hide();
            }
        }
    }
}