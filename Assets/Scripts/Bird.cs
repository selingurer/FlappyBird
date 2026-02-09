using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;


namespace DefaultNamespace
{
    public class Bird : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        [Inject] IBirdStateService _birdStateService;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }
}