using Event;
using Event.Score;

namespace Service
{
    public class ScoreService : IScoreService
    {
        private const int SCORE = 1;
        private int _currentScore;
        private int _highScore;

        public int GetScore()
        {
            return _currentScore;
        }

        public int GetHighScore()
        {
            return _highScore;
        }

        public void AddScore()
        {
            _currentScore += SCORE;

            EventBus<ScoreChanged>.Publish(new ScoreChanged
            {
                Score = _currentScore,
            });

            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
                EventBus<HighScoreChanged>.Publish(new HighScoreChanged
                {
                    HighScore = _highScore
                });
            }
        }

        public void Init(int savedHighScore)
        {
            if (_highScore == savedHighScore)
                return;

            _highScore = savedHighScore;
            EventBus<HighScoreChanged>.Publish(new HighScoreChanged
            {
                HighScore = _highScore
            });
        }

        public void Reset()
        {
            _currentScore = 0;
        }
    }

    public interface IScoreService : IResettable
    {
        public int GetScore();

        public int GetHighScore();

        public void AddScore();

        public void Init(int savedHighScore);
    }
}