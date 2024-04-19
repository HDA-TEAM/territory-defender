using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Route
{
    public class CallWaveView : MonoBehaviour
    {
        [SerializeField]
        private Button _btnCallWave;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _animationDuration;

        private Action<int> _onCallWave;
        private int _curRouteIndex;
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
            _onCallWave?.Invoke(_curRouteIndex);
        }
        public void Setup(Action<int> onCallWave, int index)
        {
            _onCallWave = onCallWave;
            _curRouteIndex = index;
        }
    }
}
