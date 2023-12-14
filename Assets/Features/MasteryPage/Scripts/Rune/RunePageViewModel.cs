using System;
using System.Collections.Generic;
using UnityEngine;

public class RunePageViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    [SerializeField] private RuneDetailViewModel _runeDetailViewModel;
    
    
    [Header("Data"), Space(12)] 
    [SerializeField] private RuneDataAsset _runeDataAsset;
    
    // Internal
    private List<RuneComposite> _runeComposites;
    private RuneDataSO _preRuneDataSo;
    private ItemRuneView _preSelectedItem;
    private ItemUpgradeRuneView _preSelectedUpgradeRuneView;
    
    private float _valueCastStringToInt;
    private void Awake()
    {
        _runeComposites = new List<RuneComposite>();

        UpdateData();
    }
    
    private void UpdateData()
    {
        List<RuneDataSO> listRuneDataSo = _runeDataAsset.GetAllRuneData();
        
        foreach (var runeDataSo in listRuneDataSo)
        {
            _runeComposites.Add(
                new RuneComposite
                {
                    RuneId = runeDataSo._runeId,
                    TypeName = runeDataSo._name,
                    AdditionalValue =  runeDataSo._additionalValue,
                    Operate = runeDataSo._operate,
                    CurrentStacks = runeDataSo._currentStacks,
                    Stacks = runeDataSo._stacks,
                    StarNeedToUpgrade = runeDataSo._starNeedToUpgrade,
                    AvatarSelected = runeDataSo._avatarSelected,
                    AvatarStarted = runeDataSo._avatarStarted
                }
            );
        }
        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = 0; i < _itemRuneViews.Count; i++)
        {
            // Setup rune view
            _itemRuneViews[i].SetRuneStacks(_runeComposites[i]);

            // Rune avatar logic
            _itemRuneViews[i].SetAvatarRune(_runeComposites[i].CurrentStacks > 0 ? _runeComposites[i].AvatarSelected : _runeComposites[i].AvatarStarted);
            
            // Selected rune setup
            _itemRuneViews[i].Setup(_runeComposites[i], OnSelectedRuneItem);
        }
    }

    private void OnSelectedRuneItem(ItemRuneView itemRuneView)
    {
        if (_preSelectedItem == null)
        {
            _runeDetailViewModel.StartSetup();
        }
        _preSelectedItem = itemRuneView;
        _runeDetailViewModel.Setup(_preSelectedItem.RuneComposite);
        
        // Update stacks data
        if (_preSelectedItem.RuneComposite.CurrentStacks < _preSelectedItem.RuneComposite.Stacks)
        {
            _valueCastStringToInt = itemRuneView.RuneComposite.CurrentStacks;
            _preRuneDataSo = _runeDataAsset.GetRune(itemRuneView.RuneComposite.RuneId);
            _preRuneDataSo._currentStacks = ++_valueCastStringToInt;
            
            // Update rune data
            _runeDataAsset.RuneUpdate(_preRuneDataSo);
            UpdateData();
        }
        
        //
    }
    
}

public struct RuneComposite
{
    public RuneId RuneId;
    public string TypeName;
    public string AdditionalValue;
    public string Operate;
    public float CurrentStacks;
    public float Stacks;
    public float StarNeedToUpgrade;
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
}
