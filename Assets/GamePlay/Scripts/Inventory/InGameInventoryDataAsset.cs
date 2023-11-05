using UnityEngine;

[CreateAssetMenu(fileName = "InGameInventoryDataAsset", menuName = "ScriptableObject/DataAsset/InGameInventoryDataAsset")]
public class InGameInventoryDataAsset : ScriptableObject
{
    [SerializeField] private int _currency;
    [SerializeField] private int _life;

    public int GetCurrencyValue() => _currency;
    public int GetLifeValue() => _life;
    
    public void TryChangeCurrency(int value)
    {
        if (_currency > value)
        {
            _currency += value;
        }
    }
    public void TryChangeLife(int value)
    {
        if (_life > value)
        {
            _life += value;
        }
        if (_life <= 0)
        {
            //todo
            //End game condition
        }
    }
}
