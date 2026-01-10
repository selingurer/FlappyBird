using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class PipePair : MonoBehaviour
{
    private const float MOVE_SPEED = 2f;
    private const float END_POSITION_X = -10;

    [Header("Refs")]
    [SerializeField] private Transform _topPipe;
    [SerializeField] private Transform _bottomPipe;

    private float _minPipeHeight = 0.5f;
    private float _maxPipeHeight = 4.5f;
    
    private Tween _tween;
    
    public void Setup(DifficultyData data)
    {
        Camera cam = Camera.main;

        float cameraHeight = cam.orthographicSize * 2f;
        float cameraCenterY = cam.transform.position.y;

        float levelBottomY = cameraCenterY - cam.orthographicSize;
        float levelTopY = cameraCenterY + cam.orthographicSize;

        float gapSize = cameraHeight * data.GapRatio;

        float centerOffset = cameraHeight * data.CenterYRatio;

        float safeMinY = levelBottomY + gapSize * 0.5f + _minPipeHeight;
        float safeMaxY = levelTopY - gapSize * 0.5f - _minPipeHeight;

        float gapCenterY = Random.Range(
            Mathf.Max(cameraCenterY - centerOffset, safeMinY),
            Mathf.Min(cameraCenterY + centerOffset, safeMaxY)
        );

        float bottomHeight =
            gapCenterY - gapSize * 0.5f - levelBottomY;

        bottomHeight = Mathf.Clamp(bottomHeight, _minPipeHeight, _maxPipeHeight);
        
        _bottomPipe.localScale = new Vector3(1, bottomHeight, 1);
        

        float topHeight =
            levelTopY - (gapCenterY + gapSize * 0.5f);
        
        topHeight = Mathf.Clamp(topHeight, _minPipeHeight, _maxPipeHeight);

        _topPipe.localScale = new Vector3(1, topHeight, 1);
  
    }


    public void MoveStart()
    {
        float distance = Mathf.Abs(transform.position.x - END_POSITION_X);
        float duration = distance / MOVE_SPEED;

        _tween = transform.DOMoveX(END_POSITION_X, duration).SetEase(Ease.Linear).OnComplete(MoveEnd);
    }

    private void MoveEnd()
    {
        _tween.Kill();
        _tween = null;
        EventBus<PipePairMoveEndEvent>.Publish(new PipePairMoveEndEvent(this));
    }

    private void OnDisable()
    {
        _tween.Kill();
        _tween = null;
    }
}