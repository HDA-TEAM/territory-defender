using UnityEngine;

[CreateAssetMenu(fileName = "InGameInventoryDataAsset", menuName = "ScriptableObject/DataAsset/InGameInventoryDataAsset")]
public class InGameInventoryDataAsset : ScriptableObject
{
    [SerializeField] private int _currency;
    [SerializeField] private int _life;

    public int GetCurrencyValue() => _currency;
    public int GetLifeValue() => _currency;
    
    public void TryChangeCurrency(int value)
    {
        if (_currency > value)
        {
            _currency += value;
        }
    }
}
