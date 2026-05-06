using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Event;
using Event.Boosters;
using ScriptableObjects;

namespace Booster
{
    public class ShieldBooster : IBooster
    {
        private readonly IBirdStateService _birdStateService;
        private CancellationTokenSource _cts;

        public BoosterType BoosterType
        {
            get => BoosterType.Shield;
        }

        public float Duration { get; private set; }

        public ShieldBooster(IBirdStateService birdStateService)
        {
            _birdStateService = birdStateService;
        }

        public void SetData(BoosterRules data)
        {
            Duration = data.Duration;
        }

        public async UniTask Activate(Bird bird)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
            
            _birdStateService.SetShield(true);
            
            EventBus<BoosterStarted>.Publish(new BoosterStarted
            {
                BoosterType = BoosterType,
                ParentVisualTransform =  bird.transform,
                BoosterDuration = Duration
            });
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Duration), cancellationToken: _cts.Token);
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                _birdStateService.SetShield(false);
                EventBus<BoosterEnded>.Publish(new BoosterEnded
                {
                    BoosterType = BoosterType
                });
            }
          
        }

        public void Deactivate()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}