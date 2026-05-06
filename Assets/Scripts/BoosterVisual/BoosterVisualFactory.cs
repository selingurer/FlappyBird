using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Shader
{
    public class BoosterVisualFactory : IBoosterVisualFactory
    {
        private Dictionary<GameObject, IBoosterVisual> _cache = new();

        public IBoosterVisual GetOrCreate(GameObject prefab)
        {
            if (_cache.TryGetValue(prefab, out var visual))
                return visual;

            var go = GameObject.Instantiate(prefab);
            go.SetActive(false);

            visual = go.GetComponent<IBoosterVisual>();

            _cache[prefab] = visual;

            return visual;
        }
    }

    public interface IBoosterVisualFactory
    {
        public IBoosterVisual GetOrCreate(GameObject prefab);
    }
}