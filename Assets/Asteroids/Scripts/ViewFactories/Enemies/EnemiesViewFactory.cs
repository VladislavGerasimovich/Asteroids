using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.ObjectPool;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ViewFactories.Enemies
{
    public class EnemiesViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private EnemyView ufoView;
        [SerializeField] private EnemyView asteroidView;
        [SerializeField] private EnemyView partOfAsteroidView;
        
        private PoolMono<EnemyView> _ufoPool;
        private PoolMono<EnemyView> _asteroidPool;
        private PoolMono<EnemyView> _partOfAsteroidPool;
        
        public void Initialize()
        {
            GameObject nloContainer = new GameObject("NloContainer");
            _ufoPool = new PoolMono<EnemyView>(ufoView, 50, nloContainer.transform);
            _ufoPool.AutoExpand = true;
            
            GameObject asteroidContainer = new GameObject("AsteroidContainer");
            _asteroidPool = new PoolMono<EnemyView>(asteroidView, 50, asteroidContainer.transform);
            
            GameObject partOfAsteroidContainer = new GameObject("PartOfAsteroidContainer");
            _partOfAsteroidPool = new PoolMono<EnemyView>(partOfAsteroidView, 200, partOfAsteroidContainer.transform);
            _partOfAsteroidPool.AutoExpand = true;
        }
        
        public EnemyView GetTemplate(Enemy enemy)
        {
            if (enemy is Ufo)
                return _ufoPool.GetFreeElement();
            if (enemy is Asteroid)
                return _asteroidPool.GetFreeElement();
            if (enemy is PartOfAsteroid)
                return _partOfAsteroidPool.GetFreeElement();
            
            return null;
        }
        
        public void Reset(EnemyView enemyView)
        {
            if (enemyView.Enemy is Ufo)
                _ufoPool.ResetElement(enemyView);
            if (enemyView.Enemy is Asteroid)
                _asteroidPool.ResetElement(enemyView);
            if (enemyView.Enemy is PartOfAsteroid)
                _partOfAsteroidPool.ResetElement(enemyView);
        }
    }
}