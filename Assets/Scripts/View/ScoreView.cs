using Service;
using UnityEngine;
using VContainer;

public class ScoreView : MonoBehaviour
{
    private const int SCORE_POINT = 1;
    
    [Inject] private readonly IScoreService _scoreService;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bird"))
        {
            _scoreService.AddScore(SCORE_POINT);
            Debug.Log($"Score: {_scoreService.GetScore()}");
        }
    }
    
}