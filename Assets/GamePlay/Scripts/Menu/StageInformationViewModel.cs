using GamePlay.Scripts.Data;
using System;
using UnityEngine;

public class StageInformationViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private StageResourceView _stageResourceView;

    [Header("Data"), Space(12)] [SerializeField]
    private InGameInventoryRuntimeData _inventoryRuntimeData;

    // tool
    // hero skill or hero selection
    
    private void Start()
    {
        UpdateView();
    }
    private void Awake()
    {
        _inventoryRuntimeData.RegisterLifeChange(OnInventoryChange);
        _inventoryRuntimeData.RegisterCurrencyChange(OnInventoryChange);
    }
    private void OnDestroy()
    {
        _inventoryRuntimeData.UnRegisterLifeChange(OnInventoryChange);
        _inventoryRuntimeData.UnRegisterCurrencyChange(OnInventoryChange);
    }
    private void OnInventoryChange(int fakeValue) => UpdateView();
    private void UpdateView()
    {
        _stageResourceView.Setup(new StageResource
        {
            CurLife = _inventoryRuntimeData.GetLifeValue(),
            TotalCoin = _inventoryRuntimeData.GetCurrencyValue(),
            CurWaveCount = 0,
            MaxWaveCount = 5,
        });
    }
}
