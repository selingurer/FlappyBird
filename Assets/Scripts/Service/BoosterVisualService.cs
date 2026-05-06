using System;
using DefaultNamespace.Shader;
using Event;
using Event.Boosters;
using ScriptableObjects;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    public class BoosterVisualService : IInitializable, IDisposable
    {
        private readonly BoosterVisualData _boosterVisualData;
        private readonly IBoosterVisualFactory _boosterVisualFactory;
        private IBoosterVisual _activeVisual;

        public void OnBoosterStarted(BoosterStarted e)
        {
            var entry = _boosterVisualData.Entries.Find(x => x.Type == e.BoosterType);
            if (entry == null)
            {
                Debug.Log(" not found booster visual");
                return;
            }

            IBoosterVisual visual = _boosterVisualFactory.GetOrCreate(entry.VisualPrefab);
           
            visual.Initialize(e.ParentVisualTransform, e.BoosterDuration);
            _activeVisual = visual;

            visual.Activate();
        }

        public void OnBoosterEnded(BoosterEnded e)
        {
            _activeVisual?.Deactivate();
            _activeVisual = null;
        }

        public BoosterVisualService(BoosterVisualData boosterVisualData, IBoosterVisualFactory factory)
        {
            _boosterVisualData = boosterVisualData;
            _boosterVisualFactory = factory;
        }

        public void Initialize()
        {
            EventBus<BoosterStarted>.Subscribe(OnBoosterStarted);
            EventBus<BoosterEnded>.Subscribe(OnBoosterEnded);
        }

        public void Dispose()
        {
            EventBus<BoosterStarted>.Unsubscribe(OnBoosterStarted);
            EventBus<BoosterEnded>.Unsubscribe(OnBoosterEnded);
        }
    }
}