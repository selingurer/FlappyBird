using UnityEngine;

public class BottomBackgroundScaleService : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
   
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

        float spriteWidth = _sprite.sprite.bounds.size.x;

        float scaleX = screenWidth / spriteWidth;

        transform.localScale = new Vector3(
            scaleX,
            1f,   // Y SABİT
            1f
        );
    }
}