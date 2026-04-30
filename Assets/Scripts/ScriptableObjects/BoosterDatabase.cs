using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    public enum BoosterType
    {
        MagnetFlight = 0,
        Shield = 1
    }

    [Serializable]
    public class BoosterRules
    {
        public BoosterType Type;
        public float Duration;
    }
    
    [CreateAssetMenu(fileName = "BoosterData", menuName = "ScriptableObjects/BoosterData")]
    public class BoosterDatabase : ScriptableObject
    {
        public List<BoosterRules> Boosters;

        public BoosterRules GetBoosterData(BoosterType boosterType)
        {
            BoosterRules data = Boosters.Find(x => x.Type == boosterType);

            if (data == null)
            {
                Debug.LogError($"Booster {boosterType} bulunamadı");
            }

            return data;
        }

    }
}