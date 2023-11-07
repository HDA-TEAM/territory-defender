using System;
using UnityEngine;
using UnityEngine.UI;

public class StageInformationViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private StageResourceView _stageResourceView;

    [Header("Data"), Space(12)] [SerializeField]
    private InGameInventoryDataAsset _inventoryDataAsset;

    // tool
    // hero skill or hero selection
    
    private void Start()
    {
        UpdateView();
    }
    private void Awake()
    {
        _inventoryDataAsset.RegisterLifeChange(OnInventoryChange);
        _inventoryDataAsset.RegisterCurrencyChange(OnInventoryChange);
    }
    private void OnDestroy()
    {
        _inventoryDataAsset.UnRegisterLifeChange(OnInventoryChange);
        _inventoryDataAsset.UnRegisterCurrencyChange(OnInventoryChange);
    }
    private void OnInventoryChange(int fakeValue) => UpdateView();
    private void UpdateView()
    {
        _stageResourceView.Setup(new StageResource
        {
            CurLife = _inventoryDataAsset.GetLifeValue(),
            TotalCoin = _inventoryDataAsset.GetCurrencyValue(),
            CurWaveCount = 0,
            MaxWaveCount = 5,
        });
    }
}
