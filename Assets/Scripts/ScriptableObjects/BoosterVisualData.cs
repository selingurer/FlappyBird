using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace.Shader
{
    [System.Serializable]
    public class BoosterVisualEntry
    {
        public BoosterType Type;
        public GameObject VisualPrefab;
    }

    [CreateAssetMenu(fileName = "BoosterVisualData", menuName = "ScriptableObjects/BoosterVisualData")]
    public class BoosterVisualData : ScriptableObject
    {
        public List<BoosterVisualEntry> Entries = new List<BoosterVisualEntry>();
    }
}