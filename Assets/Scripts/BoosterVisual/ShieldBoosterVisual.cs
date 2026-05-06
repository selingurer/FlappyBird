using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.Shader;
using UnityEngine;

namespace BoosterVisual
{
    public class ShieldBoosterVisual : MonoBehaviour, IBoosterVisual
    {
        private float _duration = 5f;
        private CancellationTokenSource _cts;

        public void Initialize(Transform boosterTransform, float duration)
        {
            _duration = duration;
            transform.SetParent(boosterTransform);
        }

        public void Activate()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }

            _cts = new CancellationTokenSource();
            RunShield(_cts.Token).Forget();
        }

        private async UniTask RunShield(CancellationToken token)
        {
            transform.localPosition = Vector3.zero;
            transform.gameObject.SetActive(true);

            float time = 0f;

            try
            {
                while (time < _duration)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);

                    time += Time.deltaTime;
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }

            Deactivate();
        }

        public void Deactivate()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            gameObject.SetActive(false);
        }
    }
}