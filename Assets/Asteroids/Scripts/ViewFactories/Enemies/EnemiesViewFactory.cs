using Asteroids.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ViewFactories.Enemies
{
    public class EnemiesViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private EnemyView NloView;
        [SerializeField] private EnemyView AsteroidView;
        [SerializeField] private EnemyView PartOfAsteroidView;
        
        private PoolMono<EnemyView> _nloPool;
        private PoolMono<EnemyView> _asteroidPool;
        private PoolMono<EnemyView> _partOfAsteroidPool;
        
        public void Initialize()
        {
            GameObject nloContainer = new GameObject("NloContainer");
            _nloPool = new PoolMono<EnemyView>(NloView, 50, nloContainer.transform);
            _nloPool.autoExpand = true;
            
            GameObject asteroidContainer = new GameObject("AsteroidContainer");
            _asteroidPool = new PoolMono<EnemyView>(AsteroidView, 50, asteroidContainer.transform);
            
            GameObject partOfAsteroidContainer = new GameObject("PartOfAsteroidContainer");
            _partOfAsteroidPool = new PoolMono<EnemyView>(PartOfAsteroidView, 200, partOfAsteroidContainer.transform);
            _partOfAsteroidPool.autoExpand = true;
        }
        
        public EnemyView GetTemplate(Enemy enemy)
        {
            if (enemy is Nlo)
                return _nloPool.GetFreeElement();
            if (enemy is Asteroid)
                return _asteroidPool.GetFreeElement();
            if (enemy is PartOfAsteroid)
                return _partOfAsteroidPool.GetFreeElement();
            
            return null;
        }
        
        public void Reset(EnemyView enemyView)
        {
            if (enemyView.Enemy is Nlo)
                _nloPool.ResetElement(enemyView);
            if (enemyView.Enemy is Asteroid)
                _asteroidPool.ResetElement(enemyView);
            if (enemyView.Enemy is PartOfAsteroid)
                _partOfAsteroidPool.ResetElement(enemyView);
        }
    }
}