using System;
using Booster;
using Event;
using Event.Boosters;
using Event.GameFlow;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Service
{
    public class BoosterPickupService : IStartable, IDisposable, IResettable
    {
        private PickupSpawnData _pickupSpawnData;
        private int _counterPipeTrigger;
        private int _indexPipe;
        private ObjectPool<Pickup> _pickupPool;
        private Pickup _pickupActive;
        private IMapService _mapService;
        
        private BoosterSpawnEntry _boosterSpawnEntry;

        [Inject]
        public BoosterPickupService(PickupSpawnData pickupSpawnData, IObjectResolver resolver, IMapService mapService)
        {
            _pickupSpawnData = pickupSpawnData;
            _pickupPool = new ObjectPool<Pickup>(resolver: resolver, _pickupSpawnData.Prefab,
                _pickupSpawnData.PickupTransform);
            _mapService = mapService;
        }

        public void Start()
        {
            EventBus<OnPipePassTrigger>.Subscribe(OnPipePassTriggerChanged);
            EventBus<PickupActive>.Subscribe(OnPickupActive);
            GetRandomPickup();
        }

        public void Dispose()
        {
            EventBus<OnPipePassTrigger>.Unsubscribe(OnPipePassTriggerChanged);
            EventBus<PickupActive>.Unsubscribe(OnPickupActive);
        }

        private void OnPipePassTriggerChanged(OnPipePassTrigger obj)
        {
            _counterPipeTrigger++;
            _indexPipe = obj.Index + 1;
            HandleBoosterSpawnCheck();
        }

        private void OnPickupActive(PickupActive obj)
        {
            _pickupPool.ReturnObject(obj.Pickup);
        }

        private void HandleBoosterSpawnCheck()
        {
            if (_counterPipeTrigger < _pickupSpawnData.StartPipeGap)
                return;


            if (_counterPipeTrigger % _boosterSpawnEntry.Rule.MinPipeGap == 0)
            {
                CreateBoosterPickup();
            }
        }

        private void CreateBoosterPickup()
        {
            var pickup = _pickupPool.GetObject();
            var pipe = _mapService.GetPipe(_indexPipe);
            pickup.transform.parent = pipe.PipePassTriggerTransform;
            pickup.transform.localPosition = new Vector3(0,0,pickup.transform.localPosition.z);
            pickup.Initialize(_boosterSpawnEntry.Booster.Type,_boosterSpawnEntry.Booster.Sprite);
            _counterPipeTrigger = 0;
            GetRandomPickup();
        }

        private void GetRandomPickup()
        {
            _boosterSpawnEntry = _pickupSpawnData.GetRandomEntry();
        }

        public void Reset()
        {
            _counterPipeTrigger = 0;
            _indexPipe = 0;

            if (_pickupActive != null)
            {
                _pickupPool.ReturnObject(_pickupActive);
            }
        }
    }
}