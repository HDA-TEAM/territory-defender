using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthComp : UnitBaseComponent
{
    [SerializeField] private float _maxHeath = 50;
    [SerializeField] private float _currentHealth = 50;
    [SerializeField] private TMP_Text _txtToast;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private CanvasGroup _healthParentCanvasGroup;
    [SerializeField] private float _unitDurationHealthChange = 2f;
    
    private Tween _tweenProgressHeal;
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsHandlerComp();
        _maxHeath = stats.GetCurrentStatValue(StatId.MaxHeal);
        _currentHealth = _maxHeath;
        _healthSlider.value = 1f;
    }
    protected override void BuffUpdate()
    {
        float curHealthUnit = _currentHealth / _maxHeath;
        var stats = _unitBaseParent.UnitStatsHandlerComp();
        _maxHeath = stats.GetCurrentStatValue(StatId.MaxHeal);
        _currentHealth = curHealthUnit * _maxHeath;
    }
    public void PlayHurting(float dame)
    {
        _currentHealth -= dame;
        SetHealthSlider();
        
        CheckDie();

        ShowToastHitting(dame);
    }
    private async void SetHealthSlider()
    {
        _healthParentCanvasGroup.alpha = 1;
        var sliderValue = (float)(_currentHealth * 1.0 / _maxHeath);
        var duration = Math.Abs(_healthSlider.value - sliderValue) * _unitDurationHealthChange;
        _tweenProgressHeal = _healthSlider.DOValue(sliderValue, duration).OnComplete(() =>
        {
            _healthParentCanvasGroup.alpha = 0;
        });
        await _tweenProgressHeal.AsyncWaitForCompletion();
    }
    private async void ShowToastHitting(float dame)
    {
        _txtToast.gameObject.SetActive(true);
        _txtToast.text = ((int)dame).ToString();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _txtToast.gameObject.SetActive(false);
    }
    private void CheckDie()
    {
        // Notify for unit observer to remove it self
        _unitBaseParent.OnOutOfHeal?.Invoke(_unitBaseParent);
        // UnitManager.Instance.NotifyAllUnit(_unitBaseParent.gameObject.tag,_unitBaseParent);
        // Notify for state machine
        _unitBaseParent.OnDie?.Invoke(_currentHealth <= 0);
    }

    public void ResetState()
    {
        gameObject.SetActive(false);
        _currentHealth = _maxHeath;
    }
}
