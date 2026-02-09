using System;
using System.Collections.Generic;
using DefaultNamespace;
using Service;

namespace ScriptableObjects
{
    using UnityEngine;

    [Serializable]
    public struct BirdStateSoundEntry
    {
        public BirdState BirdState;
        public SoundType SoundType;
    }

    [CreateAssetMenu(fileName = "Bird State Sound Map", menuName = "ScriptableObjects/Bird State Sound Map")]
    public class BirdStateSoundConfig : ScriptableObject
    {
        private BirdStateSoundEntry[] _entries;

        private Dictionary<BirdState, SoundType> _lookup;

        public IReadOnlyDictionary<BirdState, SoundType> Lookup
        {
            get
            {
                if (_lookup == null)
                    BuildLookup();

                return _lookup;
            }
        }

        private void BuildLookup()
        {
            _lookup = new Dictionary<BirdState, SoundType>();

            foreach (var entry in _entries)
            {
                if (!_lookup.ContainsKey(entry.BirdState))
                    _lookup.Add(entry.BirdState, entry.SoundType);
            }
        }
    }
}