using System;
using Event;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;


namespace DefaultNamespace
{
    public class Bird : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector3 _startPos;

        [Inject] IBirdStateService _birdStateService;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _startPos = GetBirdStartPosition(Camera.main);

            EventBus<GameRestarted>.Subscribe(OnGameReStarted);
        }

        private void OnGameReStarted(GameRestarted obj)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = _startPos;
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
            if (Keyboard.current != null &&
                Keyboard.current.spaceKey.isPressed)
            {
                Jump();
            }

            _birdStateService.EvaluateVelocity(_rigidbody2D.linearVelocity.y);
        }

        private void Jump()
        {
            _rigidbody2D.linearVelocity = Vector2.up * 3f;
        }

        private void OnDestroy()
        {
            EventBus<GameRestarted>.Unsubscribe(OnGameReStarted);
        }
    }
}