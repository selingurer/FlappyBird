using System;
using System.Threading;
using Booster;
using Cysharp.Threading.Tasks;
using Event;
using Event.GameFlow;
using ScriptableObjects;
using Service;
using UnityEngine;

public class MagnetFlightBooster : IMagnetFlight, IDisposable
{
    private const float Force = 4f;
    private const float MaxSpeed = 5f;

    private readonly IMapService _mapService;
    private int _nextPipeIndex;

    private CancellationTokenSource _cts;
    public BoosterType BoosterType { get; private set; }

    public float Duration { get; private set; }

    public MagnetFlightBooster(IMapService mapService)
    {
        _mapService = mapService;
        EventBus<OnPipePassTrigger>.Subscribe(OnPipePassTrigger);
    }

    public void SetData(BoosterRules data)
    {
        BoosterType = data.Type;
        Duration = data.Duration;
    }

    public async UniTask Activate(Bird bird)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        var rb = bird.Rigidbody;

        if (rb == null)
        {
            Debug.LogError("Rigidbody missing");
            return;
        }

        float timer = 0f;

        while (timer < Duration)
        {
            PipePair pipe = _mapService.GetPipe(_nextPipeIndex);
            float targetY = pipe.GetNextGapCenterY();

            float deltaY = targetY - rb.position.y;

            float forceY = Mathf.Clamp(deltaY, -1f, 1f) * Force;

            rb.AddForce(Vector2.up * forceY, ForceMode2D.Force);

            if (rb.linearVelocity.y > MaxSpeed)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, MaxSpeed);

            timer += Time.fixedDeltaTime;

            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, _cts.Token);
        }

        _cts = null;
    }

    public void Deactivate()
    {
        if (_cts != null)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }

    public void Dispose()
    {
        EventBus<OnPipePassTrigger>.Unsubscribe(OnPipePassTrigger);
    }

    private void OnPipePassTrigger(OnPipePassTrigger pipePassTrigger)
    {
        _nextPipeIndex = pipePassTrigger.Index + 1;
    }
}