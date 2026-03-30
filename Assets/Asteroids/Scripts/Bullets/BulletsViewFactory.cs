using Asteroids.Scripts.Bullets.Views;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Bullets
{
    public class BulletsViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private BulletView defaultBulletSprite;
        [SerializeField] private BulletView laserGunBulletSprite;
        
        private PoolMono<BulletView> _defaultBulletPool;
        private PoolMono<BulletView> _laserGunBulletPool;

        public void Initialize()
        {
            GameObject defaultBulletContainer = new GameObject("DefaultBulletContainer");
            _defaultBulletPool = new PoolMono<BulletView>(defaultBulletSprite, 50, defaultBulletContainer.transform);
            
            GameObject laserGunBulletContainer = new GameObject("LaserGunBulletContainer");
            _laserGunBulletPool = new PoolMono<BulletView>(laserGunBulletSprite, 50, laserGunBulletContainer.transform);
        }

        public BulletView GetTemplate(IBullet bullet)
        {
            if (bullet is DefaultBullet)
                return _defaultBulletPool.GetFreeElement();
            if (bullet is LaserGunBullet)
                return _laserGunBulletPool.GetFreeElement();

            return null;
        }
    }
}