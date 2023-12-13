using System;
using System.Collections.Generic;
using UnityEngine;

public class ListRuneViewModel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemRuneView> _itemRuneViews;
    [SerializeField] private RuneDetailView _runeDetailView;
    
    [Header("Data"), Space(12)] 
    [SerializeField] private RuneDataAsset _runeDataAsset;
    
    // Internal
    private List<RuneComposite> _runeComposites;
    private RuneDataSO _preRuneDataSo;
    private ItemRuneView _preSelectedItem;
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
            _runeDetailView.Setup(_runeComposites[i]);
            
            // Rune avatar logic
            _itemRuneViews[i].SetAvatarRune(_runeComposites[i].CurrentStacks > 0 ? _runeComposites[i].AvatarSelected : _runeComposites[i].AvatarStarted);
            
            // Selected rune setup
            _itemRuneViews[i].Setup(_runeComposites[i], OnSelectedRuneItem);
        }
    }

    private void OnSelectedRuneItem(ItemRuneView itemRuneView)
    {
        if (_preSelectedItem != null)
        {
            
        }
        
        // Update stacks data
        _valueCastStringToInt = itemRuneView.RuneComposite.CurrentStacks;
        _preRuneDataSo = _runeDataAsset.GetRune(itemRuneView.RuneComposite.RuneId);
        _preRuneDataSo._currentStacks = ++_valueCastStringToInt;
        
        //
        
        // Update rune data
        _runeDataAsset.RuneUpdate(_preRuneDataSo);
        UpdateData();
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
    public Sprite AvatarSelected;
    public Sprite AvatarStarted;
}
