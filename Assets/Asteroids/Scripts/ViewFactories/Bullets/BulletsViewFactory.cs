using Asteroids.Scripts.Bullets;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ViewFactories.Bullets
{
    public class BulletsViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private BulletView defaultBulletView;
        [SerializeField] private BulletView laserGunBulletView;
        
        private PoolMono<BulletView> _defaultBulletPool;
        private PoolMono<BulletView> _laserGunBulletPool;

        public void Initialize()
        {
            GameObject defaultBulletContainer = new GameObject("DefaultBulletContainer");
            _defaultBulletPool = new PoolMono<BulletView>(defaultBulletView, 50, defaultBulletContainer.transform);
            
            GameObject laserGunBulletContainer = new GameObject("LaserGunBulletContainer");
            _laserGunBulletPool = new PoolMono<BulletView>(laserGunBulletView, 50, laserGunBulletContainer.transform);
        }

        public BulletView GetTemplate(Bullet bullet)
        {
            if (bullet is DefaultBullet)
                return _defaultBulletPool.GetFreeElement();
            if (bullet is LaserGunBullet)
                return _laserGunBulletPool.GetFreeElement();

            return null;
        }
        
        public void Reset(BulletEntity bulletEntity, BulletView bulletView)
        {
            if (bulletEntity.Entity is DefaultBullet)
                _defaultBulletPool.ResetElement(bulletView);
            if (bulletEntity.Entity is LaserGunBullet)
                _laserGunBulletPool.ResetElement(bulletView);
        }
    }
}