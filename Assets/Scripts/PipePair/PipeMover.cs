using DG.Tweening;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _endX;

    private Tween _tween;

    public void Move(System.Action onComplete)
    {
        float distance = Mathf.Abs(transform.position.x - _endX);
        float duration = distance / _speed;

        _tween = transform
            .DOMoveX(_endX, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }
}