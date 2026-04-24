using System;
using UnityEngine;

public class PipePassTrigger : MonoBehaviour
{
    public event Action OnPassed;

    private bool _passed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_passed) return;

        if (other.CompareTag("Bird"))
        {
            _passed = true;
            OnPassed?.Invoke();
        }
    }
    
}