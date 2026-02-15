using DefaultNamespace;
using UnityEngine;
using VContainer.Unity;

namespace Service
{
    public class DifficultyService : ITickable , IDifficultyService
    {
        private readonly float _maxGapRatio = 0.35f;
        private readonly float _minGapRatio = 0.10f;


        private readonly float _maxCenterYRatio = 0.30f;
        private readonly float _minCenterYRatio = 0.15f;

        private readonly float _difficultyDuration = 60f;

        private float _time;

        public void Tick()
        {
            _time += Time.deltaTime;
        }

        public DifficultyData GetCurrent()
        {
            float t = Mathf.Clamp01(_time / _difficultyDuration);
           
            float gapRatio = Mathf.Lerp(_maxGapRatio, _minGapRatio, t);
            float centerYRatio = Mathf.Lerp(_maxCenterYRatio, _minCenterYRatio, t);

            return new DifficultyData
            {
                GapRatio = gapRatio,
                CenterYRatio = centerYRatio
            };
        }


        public void Reset()
        {
            _time = 0f;
        }
    }

    public interface IDifficultyService : IResettable
    {
        public DifficultyData GetCurrent();
    }
}