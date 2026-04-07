using Asteroids.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ViewFactories.Enemies
{
    public class EnemiesViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private EnemyView NloView;
        [SerializeField] private EnemyView AsteroidView;
        
        private PoolMono<EnemyView> _NloPool;
        private PoolMono<EnemyView> _AsteroidPool;
        
        public void Initialize()
        {
            GameObject nloContainer = new GameObject("NloContainer");
            _NloPool = new PoolMono<EnemyView>(NloView, 50, nloContainer.transform);
            _NloPool.autoExpand = true;
            GameObject asteroidContainer = new GameObject("AsteroidContainer");
            _AsteroidPool = new PoolMono<EnemyView>(AsteroidView, 50, asteroidContainer.transform);
        }
        
        public EnemyView GetTemplate(Enemy enemy)
        {
            if (enemy is Nlo)
                return _NloPool.GetFreeElement();
            if (enemy is Asteroid)
                return _AsteroidPool.GetFreeElement();

            return null;
        }
        
        public void Reset(EnemyView enemyView)
        {
            if (enemyView.Enemy is Nlo)
                _NloPool.ResetElement(enemyView);
            if (enemyView.Enemy is Asteroid)
                _AsteroidPool.ResetElement(enemyView);
        }
    }
}