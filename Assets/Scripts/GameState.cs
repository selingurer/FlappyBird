using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameState
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            if (IsPaused) return;

            IsPaused = true;

            Time.timeScale = 0f;
            DOTween.PauseAll();
        }

        public void Resume()
        {
            if (!IsPaused) return;

            IsPaused = false;

            Time.timeScale = 1f;
            DOTween.PlayAll();
        }
    }

}