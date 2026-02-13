using Event.ButtonClick;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private Button _playAgainButton;

        private void OnEnable()
        {
            _playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        }

        private void OnDisable()
        {
            _playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        }

        private void OnPlayAgainButtonClicked()
        {
            EventBus<PlayAgainButtonClicked>.Publish(new PlayAgainButtonClicked());
        }
    }
}