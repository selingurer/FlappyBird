using System;
using DefaultNamespace;
using DefaultNamespace.UI;
using Event.ButtonClick;
using Event.Score;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _highScoreTitleText;

        private void Awake()
        {
            EventBus<HighScoreChanged>.Subscribe(OnHighScoreChanged);
            EventBus<ScoreChanged>.Subscribe(OnScoreChanged);
            _playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        }

        private void OnHighScoreChanged(HighScoreChanged obj)
        {
            _highScoreTitleText.text = "NEW HIGH SCORE";
            _highScoreText.text = obj.HighScore.ToString();
        }

        private void OnScoreChanged(ScoreChanged obj)
        {
            _scoreText.text = obj.Score.ToString();
        }

        private void OnDisable()
        {
            _highScoreTitleText.text = "HIGH SCORE";
            _scoreText.text = "0";
        }

        private void OnDestroy()
        {
            EventBus<HighScoreChanged>.Unsubscribe(OnHighScoreChanged);
            EventBus<ScoreChanged>.Unsubscribe(OnScoreChanged);
            _playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        }

        private void OnPlayAgainButtonClicked()
        {
            EventBus<PlayAgainButtonClicked>.Publish(new PlayAgainButtonClicked());
        }
    }
}