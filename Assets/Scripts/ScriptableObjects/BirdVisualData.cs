using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BirdVisualData", menuName = "ScriptableObjects/BirdVisualData")]
    public class BirdVisualData : ScriptableObject
    {
        public Sprite SpriteMidFlap;
        public Sprite SpriteUpFlap;
        public Sprite SpriteDownFlap;
    }
}