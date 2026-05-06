using UnityEngine;

namespace DefaultNamespace.Shader
{
    
    public interface IBoosterVisual
    {
        public void Initialize(Transform parentTransform, float duration);
        public void Activate();
        public void Deactivate();
    }
}