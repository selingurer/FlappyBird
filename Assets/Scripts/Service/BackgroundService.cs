using UnityEngine;

public class BackgroundService : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        ApplyScale();
    }

    private void ApplyScale()
    {
        float screenHeight = _camera.orthographicSize * 2f;
        float screenWidth = screenHeight * _camera.aspect;

        Vector2 spriteSize = _background.sprite.bounds.size;

        float scaleX = screenWidth / spriteSize.x;
        float scaleY = screenHeight / spriteSize.y;
        
        float finalScale = Mathf.Max(scaleX, scaleY);
        
        _background.transform.localScale = new Vector3(
            finalScale,
            finalScale,
            1f
        );
    }
}