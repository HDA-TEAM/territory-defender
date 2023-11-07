using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameInventoryDataAsset", menuName = "ScriptableObject/DataAsset/InGameInventoryDataAsset")]
public class InGameInventoryDataAsset : ScriptableObject
{
    [SerializeField] private int _currency;
    [SerializeField] private int _life;

    public int GetCurrencyValue() => _currency;
    public int GetLifeValue() => _life;
    
    #region Callback
    private Action<int> _onCurrencyChange;
    private Action<int> _onLifeChange;
    public void RegisterCurrencyChange(Action<int> action) => _onCurrencyChange += action;
    public void UnRegisterCurrencyChange(Action<int> action) => _onCurrencyChange -= action;
    
    public void RegisterLifeChange(Action<int> action) => _onLifeChange += action;
    public void UnRegisterLifeChange(Action<int> action) => _onLifeChange -= action;
    #endregion
    
    public void TryChangeCurrency(int value)
    {
        if (_currency > value)
            _currency += value;
        _onCurrencyChange?.Invoke(_currency);
    }
    public void TryChangeLife(int value)
    {
        if (_life > value)
            _life += value;
        _onLifeChange?.Invoke(_life);
    }
}
