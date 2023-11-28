using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Text.RegularExpressions;
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

    private float _preSliderValue = 1f; // always full heal
    
    protected override void StatsUpdate()
    {
        var stats = _unitBaseParent.UnitStatsComp();
        _maxHeath = stats.GetStat(StatId.MaxHeal);
    }
    private float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            SetHealthSlider();
        }
    }
    public void PlayHurting(float dame)
    {
        CurrentHealth -= dame;
        
        CheckDie();
        
        ShowToastHitting(dame);
    }
    private void SetHealthSlider()
    {
        _healthParentCanvasGroup.alpha = 1;
        var sliderValue = (float)(_currentHealth * 1.0 / _maxHeath);
        var duration = Math.Abs(_preSliderValue - sliderValue);
        _healthSlider.DOValue(sliderValue, duration).OnComplete(()=>
        {
            _healthParentCanvasGroup.alpha = 0;
            _preSliderValue = sliderValue;
        });
    }
    private async void ShowToastHitting(float dame)
    {
        _txtToast.gameObject.SetActive(true);
        _txtToast.text = ((int)dame).ToString();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _txtToast.gameObject.SetActive(false);
    }
    private void CheckDie() => _unitBaseParent.OnDie?.Invoke(_currentHealth <= 0);
    public void ResetState()
    {
        gameObject.SetActive(false);
        CurrentHealth = _maxHeath;
    }
}
