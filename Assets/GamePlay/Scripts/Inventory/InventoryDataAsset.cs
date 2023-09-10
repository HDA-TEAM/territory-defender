using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataAsset", menuName = "ScriptableObject/DataAsset/InventoryDataAsset")]
public class InventoryDataAsset : ScriptableObject
{
    [SerializeField] private int _currency;
    [SerializeField] private int _life;

    public void TryChangeCurrency(int value)
    {
        if (_currency > value)
        {
            _currency += value;
        }
    }
}
