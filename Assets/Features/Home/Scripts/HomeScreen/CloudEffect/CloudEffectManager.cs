using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Features.Home.Scripts.HomeScreen.CloudEffect
{
    public class CloudEffectManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _topLeftPos;
        [SerializeField] private RectTransform _topRightPos;
        [SerializeField] private RectTransform _bottomLeftPos;
        [SerializeField] private RectTransform _bottomRightPos;
        [SerializeField] private List<RectTransform> _cloudGos;

        [SerializeField] private float _delaySpawnCloud;
        [SerializeField] private float _minCloudMovingDuration;
        [SerializeField] private float _maxCloudMovingDuration;
        private Tween MakeNewCloudTween(RectTransform cloudRect)
        {
            float delay = Random.Range(0, _delaySpawnCloud);
            float durationRd = Random.Range(_minCloudMovingDuration, _maxCloudMovingDuration);
            float desY = Random.Range(_bottomRightPos.localPosition.y, _topRightPos.localPosition.y);

            cloudRect.localPosition = new Vector3(_bottomLeftPos.localPosition.x, desY,0);
            Tween cloudMoveTween = cloudRect.transform.DOLocalMoveX(_topRightPos.localPosition.x, durationRd)
                .OnComplete(() =>
                {
                    MakeNewCloudTween(cloudRect);
                })
                .SetDelay(delay);
            return cloudMoveTween;
        }
        private Sequence _cloudMovementSequence;
        [Button("Plays")]
        private async void OnEnable()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            _cloudMovementSequence = DOTween.Sequence();
            foreach (RectTransform cloudRect in _cloudGos)
            {
                _cloudMovementSequence.Join(MakeNewCloudTween(cloudRect));
            }
            _cloudMovementSequence.Play();
        }
        private void OnDisable()
        {
            if (_cloudMovementSequence.IsActive())
            {
                _cloudMovementSequence.Kill();
            }
        }

    }
}
