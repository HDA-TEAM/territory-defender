using Cysharp.Threading.Tasks;
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
        _healthSlider.value = (float)(_currentHealth * 1.0 / _maxHeath);
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
        if (_currentHealth <= 0)
            ResetState();
    }
    private void ResetState()
    {
        gameObject.SetActive(false);
        // PoolingController.Instance.GetPooling(UnitBaseParent().CharacterConfig.).ReturnPool(this.gameObject);
        CurrentHealth = _maxHeath;
    }
}
