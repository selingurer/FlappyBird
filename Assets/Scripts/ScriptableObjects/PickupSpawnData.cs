using System;
using Booster;

namespace ScriptableObjects
{
    using UnityEngine;

    [Serializable]
    public class BoosterData
    {
        public BoosterType Type;
        public Sprite Sprite;
    }
    [Serializable]
    public class SpawnRule
    {
        public float Weight;
        public int MinPipeGap;
    }
    [Serializable]
    public class BoosterSpawnEntry
    {
        public BoosterData Booster;
        public SpawnRule Rule;
    }
    [CreateAssetMenu(fileName = "PickupSpawnData", menuName = "ScriptableObjects/PickupSpawnData")]
    public class PickupSpawnData : ScriptableObject
    {
        public int StartPipeGap;
        public Pickup Prefab;
        public Transform PickupTransform;
        public BoosterSpawnEntry[] SpawnEntries;
        
        public BoosterSpawnEntry GetRandomEntry()
        {
            float totalWeight = 0f;

            foreach (var e in SpawnEntries)
                totalWeight += e.Rule.Weight;

            float random = Random.value * totalWeight;

            foreach (var e in SpawnEntries)
            {
                if (random < e.Rule.Weight)
                    return e;

                random -= e.Rule.Weight;
            }

            return SpawnEntries[0];
        }
    }
}