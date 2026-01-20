using DefaultNamespace.Event;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private float jumpPower = 5f;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PipePair"))
                EventBus<OnTriggerEnterEvent>.Publish(default);
        }

        private void Update()
        {
            // TODO add touch input support

            if (Keyboard.current == null)
            {
                Debug.Log("KEYBOARD NULL");
                return;
            }
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rigidbody2D.linearVelocity = Vector2.up * jumpPower;
        }
    }
}