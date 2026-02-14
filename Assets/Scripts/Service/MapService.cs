using System;
using System.Collections.Generic;
using DefaultNamespace;
using VContainer;
using VContainer.Unity;

namespace Service
{
    public class MapService : IStartable, IDisposable, IResettable
    {
        private const int START_COUNT = 20;
        private const int EXPAND_COUNT = 10;
        private const float PIPE_DISTANCE = 4f;

        private readonly PipeFactory _factory;
        private readonly List<PipePair> _pipes = new();

        [Inject]
        public MapService(PipeFactory factory)
        {
            _factory = factory;
        }

        public void Start()
        {
            EventBus<PipePairMoveEndEvent>.Subscribe(OnPipeEnd);
            CreateInitial();
        }

        private void CreateInitial()
        {
            float startX = Helper.GetScreenCenterX() + 1;

            for (int i = 0; i < START_COUNT; i++)
            {
                var pipe = _factory.Create(startX + i * PIPE_DISTANCE);
                _pipes.Add(pipe);
            }
        }

        private void OnPipeEnd(PipePairMoveEndEvent e)
        {
            _factory.Return(e.Pair);
            _pipes.Remove(e.Pair);

            if (_pipes.Count % 10 == 0)
            {
                Expand();
            }
        }

        private void Expand()
        {
            float startX = _pipes[^1].transform.position.x + PIPE_DISTANCE;

            for (int i = 0; i < EXPAND_COUNT; i++)
            {
                var pipe = _factory.Create(startX + i * PIPE_DISTANCE);
                _pipes.Add(pipe);
            }
        }

        public void Reset()
        {
            foreach (var pipe in _pipes)
                _factory.Return(pipe);

            _pipes.Clear();
            CreateInitial();
        }

        public void Dispose()
        {
            EventBus<PipePairMoveEndEvent>.Unsubscribe(OnPipeEnd);
        }
    }
}