using System;
using DefaultNamespace;
using DefaultNamespace.Event;
using Event;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Service
{
    public class BirdVisualController : IStartable, IDisposable
    {
        private BirdVisualData _birdVisualData;
        private SpriteRenderer _spriteRenderer;

        [Inject]
        public BirdVisualController(BirdVisualData visualData, SpriteRenderer birdSpriteRenderer)
        {
            _birdVisualData = visualData;
            _spriteRenderer = birdSpriteRenderer;
        }

        public void Start()
        {
            EventBus<BirdStateChanged>.Subscribe(OnBirdStateChanged);
        }

        public void Dispose()
        {
            EventBus<BirdStateChanged>.Unsubscribe(OnBirdStateChanged);
        }

        private void OnBirdStateChanged(BirdStateChanged state)
        {
            _spriteRenderer.sprite = state.BirdState switch
            {
                BirdState.UpFlap => _birdVisualData.SpriteUpFlap,
                BirdState.DownFlap => _birdVisualData.SpriteDownFlap,
                BirdState.MidFlap => _birdVisualData.SpriteMidFlap,
                _ => _spriteRenderer.sprite
            };
        }
    }
}