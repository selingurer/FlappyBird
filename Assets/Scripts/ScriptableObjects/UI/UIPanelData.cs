using DefaultNamespace.UI;
using UnityEngine;

namespace ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "UIPanelData", menuName = "ScriptableObject/UIPanelData")]
    public class UIPanelData : ScriptableObject
    {
        public UIPanel GameStartPanel;
        public UIPanel GameEndPanel;
        public UIPanel GamePlayingPanel;
    }
}