using System;
using System.Collections.Generic;
using Service;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public class SoundData
    {
        public SoundType Type;
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume = 1f;
    }

    [CreateAssetMenu(fileName = "SoundDatas", menuName = "ScriptableObjects/SoundDatas")]
    public class SoundDatas : ScriptableObject
    {
        public SoundData[] soundDatas;
        private Dictionary<SoundType, SoundData> _lookup;

        public SoundData Get(SoundType type)
        {
            if (_lookup == null)
            {
                _lookup = new Dictionary<SoundType, SoundData>();
                foreach (var data in soundDatas)
                {
                    _lookup[data.Type] = data;
                }
            }

            return _lookup.TryGetValue(type, out var sound) ? sound : null;
        }
    }
}