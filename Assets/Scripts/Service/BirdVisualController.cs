using System;
using DefaultNamespace;
using DG.Tweening;
using Event;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Service
{
    public class BirdVisualController : IStartable, IDisposable
    {
        private const float ROTATION_DURATION = 0.4f;
        private const int  ROTATION_UP_VALUE = 30;
        private const int ROTATION_DOWN_VALUE = -30;
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
            Vector3 rotation = state.BirdState switch
            {
                BirdState.UpFlap => new Vector3(0, 0, ROTATION_UP_VALUE),
                BirdState.DownFlap => new Vector3(0, 0, ROTATION_DOWN_VALUE),
                _ => Vector3.zero
            };

            _spriteRenderer.transform.DOLocalRotate(rotation,ROTATION_DURATION);
        }
    }
}