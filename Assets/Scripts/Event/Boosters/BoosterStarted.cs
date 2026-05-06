using ScriptableObjects;
using UnityEngine;

namespace Event.Boosters
{
    public struct BoosterStarted
    {
        public BoosterType  BoosterType;
        public Transform ParentVisualTransform;
        public float BoosterDuration;
    }

    public struct BoosterEnded
    {
        public BoosterType  BoosterType;
    }
}