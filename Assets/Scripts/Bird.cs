using DefaultNamespace;
using Service;
using UnityEngine;
using VContainer;

public class Bird : MonoBehaviour, IResettable
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

    private Vector3 _startPos;

    [Inject] private IBirdStateService _birdStateService;
    [Inject] private ITouchService _touchService;

    private void Awake()
    {
        _startPos = GetBirdStartPosition(Camera.main);
        _touchService.OnTap += Jump;
    }

    private static Vector3 GetBirdStartPosition(Camera cam)
    {
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        float leftEdge = cam.transform.position.x - width / 2f;

        float x = leftEdge + width * 0.2f;
        float y = cam.transform.position.y;

        return new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PipePair"))
            _birdStateService.OnDeath();
    }

    private void FixedUpdate()
    {
        _birdStateService.EvaluateVelocity(Rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        Rigidbody.linearVelocity = Vector2.up * 3f;
    }

    public void Reset()
    {
        Rigidbody.linearVelocity = Vector2.zero;
        transform.position = _startPos;
    }

    private void OnDestroy()
    {
        if (_touchService != null)
            _touchService.OnTap -= Jump;
    }
}