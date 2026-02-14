using System;
using DefaultNamespace;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class Bird : MonoBehaviour, IResettable
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 _startPos;

    [Inject] private IBirdStateService _birdStateService;
    [Inject] private ITouchService _touchService;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        _birdStateService.EvaluateVelocity(_rigidbody2D.linearVelocity.y);
    }

    private void Jump()
    {
        _rigidbody2D.linearVelocity = Vector2.up * 3f;
    }

    public void Reset()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        transform.position = _startPos;
    }

    private void OnDestroy()
    {
        _touchService.OnTap -= Jump;
    }
}