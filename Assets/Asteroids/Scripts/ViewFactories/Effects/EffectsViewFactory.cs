using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.ViewFactories.Effects
{
    public class EffectsViewFactory : MonoBehaviour, IInitializable
    {
        [SerializeField] private EffectView InvulnerabilityEffectView;
        
        private PoolMono<EffectView> _invulnerabilityEffectsPool;
        
        public void Initialize()
        {
            GameObject invulnerabilityEffectContainer = new GameObject("InvulnerabilityEffectContainer");
            _invulnerabilityEffectsPool = new PoolMono<EffectView>(InvulnerabilityEffectView, 5, invulnerabilityEffectContainer.transform);
            _invulnerabilityEffectsPool.autoExpand = true;
        }
        
        public EffectView GetTemplate(Effects effect)
        {
            if (effect is Effects.Invulnerability)
                return _invulnerabilityEffectsPool.GetFreeElement();
            
            return null;
        }
        
        public void Reset(EffectView effectView)
        {
            if(effectView.Effect is Effects.Invulnerability)
                _invulnerabilityEffectsPool.ResetElement(effectView);
        }
    }

    public enum Effects
    {
        Invulnerability
    }
}