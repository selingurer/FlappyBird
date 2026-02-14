using System;
using DefaultNamespace;
using DefaultNamespace.UI;
using Event.Score;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GamePlayingPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            EventBus<ScoreChanged>.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChanged scoreData)
        {
            _scoreText.text = scoreData.Score.ToString();
        }

        private void OnDisable()
        {
            _scoreText.text = "";
        }

        private void OnDestroy()
        {
            EventBus<ScoreChanged>.Unsubscribe(OnScoreChanged);
        }
    }
}