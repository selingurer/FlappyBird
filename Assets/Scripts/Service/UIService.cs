using System.Collections.Generic;
using DefaultNamespace.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ScriptableObjects.UI
{
    public class UIService : IUIService
    {
        private UIPanelData _panelData;
        private Dictionary<UIPanel, ObjectPool<UIPanel>> _pools;
        private List<UIPanel> _activeAllPanels = new List<UIPanel>();
        private Transform _canvasTransform;
        private IObjectResolver _resolver;

        [Inject]
        public UIService(UIPanelData panelData, Transform canvasTransform, IObjectResolver resolver)
        {
            _panelData = panelData;
            _pools = new Dictionary<UIPanel, ObjectPool<UIPanel>>();
            _canvasTransform = canvasTransform;
            _resolver = resolver;

            RegisterPool(_panelData.GameStartPanel);
            RegisterPool(_panelData.GameEndPanel);
        }

        private void RegisterPool(UIPanel prefab)
        {
            if (!_pools.ContainsKey(prefab))
            {
                var pool = new ObjectPool<UIPanel>(_resolver, prefab, _canvasTransform, initialSize: 1, maxCount: 1);
                _pools.Add(prefab, pool);
            }
        }

        public UIPanel ShowPanel(UIPanel prefab)
        {
            if (!_pools.TryGetValue(prefab, out var pool))
            {
                RegisterPool(prefab);
                pool = _pools[prefab];
            }

            UIPanel panel = pool.GetObject();
            panel.Show();
            _activeAllPanels.Add(panel);
            return panel;
        }

        public void HidePanel(UIPanel panel)
        {
            panel.Hide();

            foreach (var kvp in _pools)
            {
                if (kvp.Key.GetType() == panel.GetType())
                {
                    kvp.Value.ReturnObject(panel);
                    break;
                }
            }
        }

        public void ClosedAllPanel()
        {
            foreach (var panel in _activeAllPanels)
            {
                HidePanel(panel);
            }
        }
    }

    public interface IUIService
    {
        public UIPanel ShowPanel(UIPanel panel);
        public void HidePanel(UIPanel panel);
        public void ClosedAllPanel();
    }
}