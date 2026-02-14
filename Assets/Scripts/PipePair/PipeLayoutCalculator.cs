using DefaultNamespace;
using UnityEngine;

public class PipeLayoutCalculator
{
    private readonly Camera _camera;

    public PipeLayoutCalculator(Camera camera)
    {
        _camera = camera;
    }

    public PipeLayout Calculate(DifficultyData data)
    {
        float camHalfHeight = _camera.orthographicSize;
        float camCenterY = _camera.transform.position.y;

        float bottomY = camCenterY - camHalfHeight;
        float topY = camCenterY + camHalfHeight;

        float gapSize = camHalfHeight * 2f * data.GapRatio;
        
        float gapCenterY = Random.Range(
            bottomY + gapSize * 0.5f,
            topY - gapSize * 0.5f);

        return new PipeLayout
        {
            GapSize = gapSize,
            CenterY = gapCenterY
        };
    }
}