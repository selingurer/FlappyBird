using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PipePair : MonoBehaviour
{
    private const float MOVE_SPEED = 2f;
    private const float END_POSITION_X = -10;
    private const float COLLIDER_BOTTOM_OFFSET_X = 0;

    [Header("Refs")] [SerializeField] private SpriteRenderer _spriteRendererTop;
    [SerializeField] private SpriteRenderer _spriteRendererBottom;
    [SerializeField] private BoxCollider2D _collider2DTop;
    [SerializeField] private BoxCollider2D _collider2DBottom;
    [SerializeField] private SpriteRenderer _spriteRendererMiddle;
    [SerializeField] private BoxCollider2D _collider2DMiddle;

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

        UpdatePipeSize(bottomHeight, _spriteRendererBottom, _collider2DBottom,
            new Vector2(COLLIDER_BOTTOM_OFFSET_X, bottomHeight / 2));

        float topHeight =
            levelTopY - (gapCenterY + gapSize * 0.5f);

        topHeight = Mathf.Clamp(topHeight, _minPipeHeight, _maxPipeHeight);

        UpdatePipeSize(topHeight, _spriteRendererTop, _collider2DTop,
            new Vector2(COLLIDER_BOTTOM_OFFSET_X, -(topHeight / 2)));
        
        float middleOffsetY = gapCenterY - transform.position.y;

        UpdatePipeSize(
            gapSize,
            _spriteRendererMiddle,
            _collider2DMiddle,
            new Vector2(COLLIDER_BOTTOM_OFFSET_X, middleOffsetY)
        );
    }

    private void UpdatePipeSize(float height, SpriteRenderer spriteRenderer, BoxCollider2D boxCollider2D,
        Vector2 offset)
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, height);
        boxCollider2D.size = spriteRenderer.size;
        boxCollider2D.offset = offset;
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
        if (_tween == null)
            return;

        _tween.Kill();
        _tween = null;
    }
}