using ScriptableObjects;
using UnityEngine;

namespace Sound
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private AudioSource _source;

        public void Play(SoundData data)
        {
            _source.pitch = data.Pitch;
            _source.PlayOneShot(data.Clip, data.Volume);
        }
    }

    public interface IAudioPlayer
    {
        void Play(SoundData data);
    }
}