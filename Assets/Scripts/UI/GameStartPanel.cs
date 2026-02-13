using Event.ButtonClick;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class GameStartPanel : UIPanel
    {
        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnClickedStartButton);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnClickedStartButton);
        }

        private void OnClickedStartButton()
        {
           EventBus<StartGameButtonClicked>.Publish(new StartGameButtonClicked());
        }
    }
}