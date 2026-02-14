using ScriptableObjects;
using Sound;
using UnityEngine;

namespace Service
{
    public enum SoundType
    {
        BirdFlapUp = 0,
        BirdFlapDown = 1,
        BirdDead = 2,

        ButtonClick = 3,
        ButtonBack = 4,

        GameStart = 5,
        GameOver
    }

    public class SoundService : ISoundService
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly SoundDatas _sounds;

        public SoundService(
            IAudioPlayer audioPlayer,
            SoundDatas sounds)
        {
            _audioPlayer = audioPlayer;
            _sounds = sounds;
        }

        public void Play(SoundType type)
        {
            var soundData = _sounds.Get(type);
            
            if (soundData == null)
                Debug.Log("Sound doesn't exist");
            
            _audioPlayer.Play(_sounds.Get(type));
        }
    }

    public interface ISoundService
    {
        void Play(SoundType type);
    }
}