using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Scripts.Character.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Character
{
    public class HealthComp : UnitBaseComponent
    {
        [SerializeField] private float _maxHeath = 50;
        [SerializeField] private float _currentHealth = 50;
        [SerializeField] private TMP_Text _txtToast;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private CanvasGroup _healthParentCanvasGroup;
        [SerializeField] private float _unitDurationHealthChange = 1f;
        [SerializeField] private float _unitDurationShowHealth = 2f;

        private Tween _tweenProgressHealChange;
        private Tween _tweenShowHealthBar;
        private Sequence _sequenceProgressHeal;
        protected override void StatsUpdate()
        {
            StatsHandlerComponent stats = _unitBaseParent.UnitStatsHandlerComp();
            _maxHeath = stats.GetCurrentStatValue(StatId.MaxHeal);
            _currentHealth = _maxHeath;
            _healthSlider.value = 1f;
        }
        protected override void BuffUpdate()
        {
            float curHealthUnit = _currentHealth / _maxHeath;
            StatsHandlerComponent stats = _unitBaseParent.UnitStatsHandlerComp();
            _maxHeath = stats.GetCurrentStatValue(StatId.MaxHeal);
            _currentHealth = curHealthUnit * _maxHeath;
        }
        public void PlayHurting(float dame, string attackSource)
        {
            _currentHealth -= dame;
            
            SetHealthSlider();

            CheckDie();

            ShowToastHitting(dame);
        }
        private void SetHealthSlider()
        {
            _sequenceProgressHeal?.Kill();
            _sequenceProgressHeal = DOTween.Sequence();

            float sliderValue = (float)(_currentHealth * 1.0 / _maxHeath);

            _tweenProgressHealChange = _healthSlider.DOValue(sliderValue, _unitDurationHealthChange);

            _tweenShowHealthBar = DOVirtual.DelayedCall(_unitDurationShowHealth, () =>
            {
                _healthParentCanvasGroup.alpha = 0;
            });
            _sequenceProgressHeal.Append(_tweenProgressHealChange);
            _sequenceProgressHeal.Append(_tweenShowHealthBar);
            _sequenceProgressHeal.OnKill(() =>
            {
                _healthSlider.value = sliderValue;
                _healthParentCanvasGroup.alpha = 0;
            });
            _sequenceProgressHeal.OnUpdate(() =>
            {
                _healthParentCanvasGroup.alpha = 1;
            });
            _sequenceProgressHeal.Play();
        }
        private async void ShowToastHitting(float dame)
        {
            _txtToast.gameObject.SetActive(true);
            _txtToast.text = ((int)dame).ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            if (_txtToast)
                _txtToast.gameObject.SetActive(false);
        }
        public bool IsDie()
        {
            return _currentHealth <= 0;
        }
        private bool CheckDie()
        {
            bool isDie = _currentHealth <= 0;
            // Notify for unit observer to remove it self
            _unitBaseParent.OnOutOfHeal?.Invoke(_unitBaseParent);
            // UnitManager.Instance.NotifyAllUnit(_unitBaseParent.gameObject.tag,_unitBaseParent);
            // Notify for state machine
            _unitBaseParent.OnDie?.Invoke(isDie);
            return isDie;
        }
        private void OnDisable()
        {
            _sequenceProgressHeal?.Kill();
        }
        public void ResetState()
        {
            gameObject.SetActive(false);
            _currentHealth = _maxHeath;
        }
    }
}
