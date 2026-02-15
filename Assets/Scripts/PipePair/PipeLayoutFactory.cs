using DefaultNamespace;
using Service;
using UnityEngine;
using VContainer;

public class PipeLayoutFactory
{
    private readonly IDifficultyService _difficulty;

    [Inject]
    public PipeLayoutFactory(IDifficultyService difficulty)
    {
        _difficulty = difficulty;
    }

    public PipeLayout Create()
    {
     
            var data = _difficulty.GetCurrent();

            float screenHeight = Helper.GetScreenHeight();
            float halfScreen = screenHeight * 0.5f;

            float gap = screenHeight * data.GapRatio;

            float centerY = Random.Range(
                -halfScreen + gap,
                halfScreen - gap);

            return new PipeLayout
            {
                CenterY = centerY,
                GapSize = gap
            };
        

    }
}