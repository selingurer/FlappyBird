using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MapService : IStartable
{
    private const int PIPE_DISTANCE = 5;
    private const int START_PIPE_COUNT = 20;
    private const int EXPANDED_PIPE_COUNT = 10;

    private readonly IDifficultyService _difficultyService;
    private List<PipePair> _pipePairs = new();
    private ObjectPool<PipePair> _pool;
    private PipePair _pair;
    private Transform _pairTransform;
    private int _returnPipeCount;

    [Inject]
    public MapService(IDifficultyService difficultyService, Transform transform, PipePair pair)
    {
        _difficultyService = difficultyService;
        _pair = pair;
        _pairTransform = transform;
    }

    public void Start()
    {
        RegisterEvents();
        _pool = new ObjectPool<PipePair>(_pair, _pairTransform);
        CreateMap();
    }

    private void RegisterEvents()
    {
        EventBus<PipePairMoveEndEvent>.Subscribe(OnPipePairMoveEndEvent);
    }

    private void UnregisterEvents()
    {
        EventBus<PipePairMoveEndEvent>.Unsubscribe(OnPipePairMoveEndEvent);
    }

    private void OnPipePairMoveEndEvent(PipePairMoveEndEvent pairMoveEndEvent)
    {
        _pool.ReturnObject(pairMoveEndEvent.Pair);
        _pipePairs.Remove(pairMoveEndEvent.Pair);
        _returnPipeCount++;
        if (_returnPipeCount % 10 == 0)
        {
            ExpandMap();
        }
    }

    private void CreateMap()
    {
        float screenCenterX = Helper.GetScreenCenterX();

        CreatePipePair(START_PIPE_COUNT, screenCenterX);
    }

    private void CreatePipePair(int size, float defaultPosX)
    {
        DifficultyData data = _difficultyService.GetCurrent();
        for (int i = 0; i < size; i++)
        {
            var obj = _pool.GetObject();
            Vector3 pos = obj.transform.position;
            pos.x = defaultPosX + PIPE_DISTANCE * i;
            obj.transform.position = pos;
            obj.Setup(data);
            obj.MoveStart();
            _pipePairs.Add(obj);
        }
    }

    private void ExpandMap()
    {
        float startPosX = _pipePairs[^1].transform.position.x + PIPE_DISTANCE;
        CreatePipePair(EXPANDED_PIPE_COUNT, startPosX);
    }
}