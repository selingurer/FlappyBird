using System;
using DefaultNamespace;
using DefaultNamespace.Event;
using ScriptableObjects;
using Service;
using VContainer;
using VContainer.Unity;

namespace Sound
{
    public class BirdSoundHandler : IStartable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly BirdStateSoundConfig _birdStateSoundConfig;

        [Inject]
        public BirdSoundHandler(ISoundService soundService, BirdStateSoundConfig stateSoundConfig)
        {
            _soundService = soundService;
            _birdStateSoundConfig = stateSoundConfig;
        }

        public void Start()
        {
           
            EventBus<BirdStateChanged>.Subscribe(OnBirdState);
        }

        public void Dispose()
        {
            EventBus<BirdStateChanged>.Unsubscribe(OnBirdState);
        }

        private void OnBirdState(BirdStateChanged stateData)
        {
            if (_birdStateSoundConfig.Lookup.TryGetValue(stateData.BirdState, out var sound))
                _soundService.Play(sound);
        }
    }
}