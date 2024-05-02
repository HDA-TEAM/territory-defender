using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Route
{
    public class CallWaveBtnView : MonoBehaviour
    {
        [SerializeField]
        private Button _btnCallWave;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _animationDuration;

        private Action _onCallWave;
        private Tween _tweenScale;
        private void Start()
        {
            _btnCallWave.onClick.AddListener(OnClickCallWave);
            _tweenScale = DOVirtual.Float(0f, 1f, _animationDuration, (t) =>
            {
                _btnCallWave.transform.localScale = Vector3.one * t;
            }).SetEase(_animationCurve).SetLoops(-1);
        }
        private void OnDestroy()
        {
            _tweenScale.Kill();
        }
        private void OnClickCallWave()
        {
            _onCallWave?.Invoke();
        }
        public void Setup(Action onCallWaveClick)
        {
            _onCallWave = onCallWaveClick;
        }
    }
}
