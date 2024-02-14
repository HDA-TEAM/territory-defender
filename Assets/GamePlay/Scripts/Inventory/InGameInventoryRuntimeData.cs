using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameInventoryRuntimeData", menuName = "ScriptableObject/Data/InGameInventoryRuntimeData")]
public class InGameInventoryRuntimeData : ScriptableObject
{
    [SerializeField] private int _currency;
    [SerializeField] private int _life;
    [SerializeField] private int _star;
    
    public int GetCurrencyValue() => _currency;
    public int GetLifeValue() => _life;
    public int GetStarValue() => _star;
    
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
    
    public void TryChangeStar(int starNumber)
    {
        _star -= starNumber;
        Debug.Log("Subtract star");
    }
}
