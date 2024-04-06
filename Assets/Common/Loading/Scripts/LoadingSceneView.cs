using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Loading.Scripts
{
    public class LoadingSceneView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textProgressPercent;
        [SerializeField] private string _formatTextProgressPercent = "{0}%";
        public void UpdateProgressBar(float value, float durationPerUnitProgressBar)
        {
            float deltaVal = value - _slider.value;
            if (deltaVal < 0)
                deltaVal = 0f;

            _slider.DOValue(value, deltaVal * durationPerUnitProgressBar);

            UpdatePercentText(value);
        }
        private void UpdatePercentText(float value)
        {
            int updateVal = (int)(value * 100);
            _textProgressPercent.text = String.Format(_formatTextProgressPercent,updateVal);
        }
        public void PlayDoFadeEffect(float startVal, float endVal, float duration)
        {
            
            _canvasGroup.alpha = startVal;
            _canvasGroup.DOFade(endVal, duration);
        }
        public void SetupBlockRaycast(bool isTurnOn)
        {
            _canvasGroup.blocksRaycasts = isTurnOn;
        }
    }
}
